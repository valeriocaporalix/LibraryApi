using LibraryApi.Models.Books;

namespace LibraryApi.Services.Interfaces
{
    public interface IBookService
    {
        public void AddBook(Book newBook);
        public void DeleteBook(int bookId);
        public IEnumerable<Book> GetAllBooks();
        public BookDetails GetBookById(int bookId);
        public BooksMostAndLessBorrow GetBookMostAndLessBorrowed();
        public List<Book> GetMostBorrowedBooks();
        public Book UpdateBook(int bookId, Book book);
    }
}
