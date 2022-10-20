using LibraryApi.Enumerators;
using LibraryApi.Models.Borrows;

namespace LibraryApi.Models.Books
{
    public class BookDetails
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public GenreEnum Genre { get; set; }

        public Borrow? ActualBorrow { get; set; }

    }
}
