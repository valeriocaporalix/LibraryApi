namespace LibraryApi.Models
{
    public class BorrowDetails
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public Book Book { get; set; }

        public DateTime BorrowStart { get; set; }

        public DateTime? BorrowEnd { get; set; } = null;
    }
}
