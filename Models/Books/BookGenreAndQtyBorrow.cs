using LibraryApi.Enumerators;

namespace LibraryApi.Models.Books
{
    public class BookGenreAndQtyBorrow
    {
        public GenreEnum Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public BookGenreAndQtyBorrow(GenreEnum id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }
    }
}
