using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BookCollection.Core;

namespace BookCollection.Tests
{
    public abstract class BookTests
    {
        protected BookTests(DbContextOptions<BookCollectionContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }
        protected DbContextOptions<BookCollectionContext> ContextOptions { get; }
        [Fact]
        public async void GetAll()
        {
            using (var context = new BookCollectionContext(ContextOptions))
            {
                IBookService bookService = new BookService(context);
                var books = (await bookService.GetAll()).ToList();

                Assert.NotNull(books);
                Assert.NotEmpty(books);
                Assert.True(books.Count == 6);
            }
        }

        [Fact]
        public async void Get()
        {
            using (var context = new BookCollectionContext(ContextOptions))
            {
                IBookService BookService = new BookService(context);
                int bookId = 1;

                var book = await BookService.Get(bookId);

                Assert.NotNull(book);
                Assert.Equal(bookId, book.Id);
            }
        }

        [Fact]
        public async void Add()
        {
            using (var context = new BookCollectionContext(ContextOptions))
            {
                IBookService BookService = new BookService(context);

                Book newBook = new Book
                {
                    Title = "Title1",
                    Author = "AuthorA",
                    Description = "Some description"
                };

                Book addedBook = await BookService.Add(newBook);
                context.Books.Remove(addedBook);
                await context.SaveChangesAsync();

                Assert.True(addedBook.Id > 0);
            }
        }

        [Fact]
        public async void Update()
        {
            using (var context = new BookCollectionContext(ContextOptions))
            {
                IBookService BookService = new BookService(context);

                Book bookToUpdate = new Book
                {
                    Id = 1,
                    Title = "Title2",
                    Author = "AuthorB",
                    Description = "Some description"
                };

                var result = await BookService.Update(bookToUpdate);

                var updatedBook = await context.Books.SingleOrDefaultAsync(i => i.Id == 1);

                Assert.Equal("Success", result);
                Assert.Equal("Title2", updatedBook.Title);
            }
        }

        [Fact]
        public async void Delete()
        {
            using (var context = new BookCollectionContext(ContextOptions))
            {
                IBookService BookService = new BookService(context);

                Book newBook = new Book
                {
                    Title = "Title3",
                    Author = "AuthorC",
                    Description = "Some description"
                };

                context.Books.Add(newBook);
                await context.SaveChangesAsync();

                var deletedBook = await BookService.Delete(newBook.Id);

                Assert.Equal(deletedBook.Id, newBook.Id);
                Assert.False(context.Books.Any(book => book.Id == deletedBook.Id));
            }
        }
    }
    public class Tests : BookTests
    {
        public Tests()
            : base(
                new DbContextOptionsBuilder<BookCollectionContext>()
                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookCollection")
                    .Options)
        {
        }
    }
}
