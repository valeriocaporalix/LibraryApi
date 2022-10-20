using LibraryApi.Models.Books;
using LibraryApi.Services;
using LibraryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            IEnumerable<Book> bookList = _bookService.GetAllBooks();
            return Ok(bookList);
        }

        [HttpGet("{bookId}")]
        public IActionResult GetDetails(int bookId)
        {
            BookDetails book = _bookService.GetBookById(bookId);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpGet("mostborrow/books")]
        public IActionResult GetFilteredByMostBorrow()
        {
            List<Book> records = _bookService.GetMostBorrowedBooks();
            if (records == null)
                return NotFound();
            return Ok(records);
        }

        [HttpGet("getmostandlessborrow/books")]
        public IActionResult GetFilteredByMostAndLessBorrows()
        {
            BooksMostAndLessBorrow records = _bookService.GetBookMostAndLessBorrowed();
            if (records == null)
                return NotFound();
            return Ok(records);
        }


        [HttpPost()]
        public IActionResult Post([FromBody] Book newBook)
        {
            _bookService.AddBook(newBook);
            return Created($"/{newBook.Id}", newBook);
        }

        [HttpDelete("{bookId}")]
        public IActionResult Delete(int bookId)
        {
            _bookService.DeleteBook(bookId);
            return NoContent();
        }

        [HttpPut("{bookId}")]
        public IActionResult Put([FromBody] Book book, int bookId)
        {
            var bookToUpdate = _bookService.UpdateBook(bookId, book);
            if (bookToUpdate != null)
                return Created($"/{bookId}", book);
            return NoContent();
        }

    }
}
