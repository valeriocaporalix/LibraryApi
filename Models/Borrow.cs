namespace LibraryApi.Models
{
    public class Borrow
    {
        public int Id { get; set; } = 50000;

        public int CustomerId { get; set; }

        public int BookId { get; set; }

        public DateTime BorrowStart { get; set; }

        public DateTime? BorrowEnd { get; set; } = null;
        
    }
}
