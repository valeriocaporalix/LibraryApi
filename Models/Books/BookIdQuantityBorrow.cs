namespace LibraryApi.Models.Books
{
    public class BookIdQuantityBorrow
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public BookIdQuantityBorrow(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
