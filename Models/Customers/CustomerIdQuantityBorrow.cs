namespace LibraryApi.Models.Customers
{
    public class CustomerIdQuantityBorrow
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public CustomerIdQuantityBorrow(int id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
