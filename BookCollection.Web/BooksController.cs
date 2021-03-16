using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCollection.Core;

namespace BookCollection.Web
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _BookService;

        public BooksController(IBookService BookService)
        {
            _BookService = BookService;
        }

        /// <summary>
        ///     GET: books
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetBooks()
        {
            return (await _BookService.GetAll())
                .Select(book => new BookDto(book));
        }

        /// <summary>
        ///     GET: books/5
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook([FromRoute] int id)
        {
            var book = await _BookService.Get(id);

            if (book == null)
                return NotFound();

            return Ok(new BookDto(book));
        }

        /// <summary>
        ///     POST: books
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostBook([FromBody] Book book)
        {
            var result = await _BookService.Add(book);

            if (result == null)
                return BadRequest("Invalid device ID");
            else
                return CreatedAtAction("PostBook", new { id = book.Id }, new BookDto(book));
        }

        /// <summary>
        ///     PUT: books/5
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Book book)
        {
            string result = await _BookService.Update(book);
            if (result == "Success")
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        ///     DELETE: books/5
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var book = await _BookService.Delete(id);

            if (book == null)
                return NotFound();

            return Ok(new BookDto(book));
        }
    }
}
