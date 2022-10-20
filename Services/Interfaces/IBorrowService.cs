using LibraryApi.Models.Borrows;

namespace LibraryApi.Services.Interfaces
{
    public interface IBorrowService
    {
        public Borrow AddBorrow(Borrow newBorrow);
        public void DeleteBorrow(int borrowId);
        public IEnumerable<Borrow> GetAllBorrow();
        public IEnumerable<Borrow> GetAllBorrowActive();
        public IEnumerable<Borrow> GetAllBorrowByBookId(int bookId);
        public IEnumerable<Borrow> GetAllBorrowByCustomerId(int customerId);
        public IEnumerable<Borrow> GetAllBorrowInRange(DateTime start, DateTime end);
        public BorrowDetails GetBorrowById(int borrowId);
        public Borrow UpdateBorrow(int borrowId, Borrow borrow);
        public Borrow UpdateEndBorrow(int borrowId, DateTime endDate);
    }
}
