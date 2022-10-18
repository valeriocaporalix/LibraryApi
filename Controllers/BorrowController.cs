using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BorrowController : Controller
    {
        
        private BorrowService _borrowService = new BorrowService();
    }
}
