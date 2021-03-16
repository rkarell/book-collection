using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookCollection.Core
{
    public class BookService : IBookService
    {
        private readonly BookCollectionContext _context;

        public BookService(BookCollectionContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await Task.FromResult(_context.Books);
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> Add(Book Book)
        {
            _context.Books.Add(Book);
            await _context.SaveChangesAsync();
            return (Book);
        }

        public async Task<string> Update(Book Book)
        {
            _context.Entry(Book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.Id))
                    return "NotFound";
                else
                    throw;
            }
        }

        public async Task<Book> Delete(int id)
        {
            var Book = await _context.Books.FindAsync(id);
            if (Book == null)
            {
                return null;
            }

            _context.Books.Remove(Book);
            await _context.SaveChangesAsync();

            return (Book);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(mw => mw.Id == id);
        }
    }
}