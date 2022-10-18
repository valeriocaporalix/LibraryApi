using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private BookService _bookService =new BookService();


    }
}
