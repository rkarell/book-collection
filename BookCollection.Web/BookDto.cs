using BookCollection.Core;

namespace BookCollection.Web
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public BookDto(Book book)
        {
            this.Id = book.Id;
            this.Title = book.Title;
            this.Author = book.Author;
            this.Description = book.Description;
        }
    }
}
