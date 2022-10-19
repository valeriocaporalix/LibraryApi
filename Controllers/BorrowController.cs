using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BorrowController : Controller
    {
        
        private BorrowService _borrowService = new BorrowService();

        [HttpGet()]
        public IActionResult GetAll()
        {
            IEnumerable<Borrow> borrowList = _borrowService.GetAllBorrow();
            return Ok(borrowList);
        }

        [HttpGet("activeBorrow")]
        public IActionResult GetAllActive()
        {
            IEnumerable<Borrow> borrowList = _borrowService.GetAllBorrowActive();
            return Ok(borrowList);
        }

        [HttpGet("{borrowId}")]
        public IActionResult GetDetails(int borrowId)
        {
            BorrowDetails borrow = _borrowService.GetBorrowById(borrowId);
            if (borrow == null)
                return NotFound();
            return Ok(borrow);
        }

        [HttpPost()]
        public IActionResult Post([FromBody] Borrow newBorrow)
        {
            _borrowService.AddBorrow(newBorrow);
            return Created($"/{newBorrow.Id}", newBorrow);
        }

        [HttpDelete("{borrowId}")]
        public IActionResult Delete(int borrowId)
        {
            _borrowService.DeleteBorrow(borrowId);
            return NoContent();
        }

        [HttpPut("{borrowId}")]
        public IActionResult Put([FromBody] Borrow borrow, int borrowId)
        {
            var borrowToUpdate = _borrowService.UpdateBorrow(borrowId, borrow);
            if (borrowToUpdate != null)
                return Created($"/{borrowId}", borrow);
            return NoContent();
        }

        [HttpPut("{borrowId}/EndDate/{endDate}")]
        public IActionResult PutDateTime(int borrowId, DateTime endDate)
        {
            var borrowToUpdate = _borrowService.UpdateEndBorrow(borrowId, endDate);
            if (borrowToUpdate != null)
                return Created($"/{borrowId}", borrowToUpdate);
            return NotFound();
        }
    }
}
