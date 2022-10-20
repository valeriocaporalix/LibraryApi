using LibraryApi.Enumerators;

namespace LibraryApi.Models.Books
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public GenreEnum Genre { get; set; }

    }
}
