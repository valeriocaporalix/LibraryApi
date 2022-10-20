namespace LibraryApi.Models.Books
{
    public class BooksMostAndLessBorrow
    {

        public int MostBorrowedQty { get; set; }
        public List<Book> BookMostBorrowed { get; set; }

        public int LessBorrowedQty { get; set; }

        public List<Book> BookLessBorrower { get; set; }

        public BooksMostAndLessBorrow(int mostBorrowedQty, 
            List<Book> bookMostBorrowed, 
            int lessBorrowedQty, 
            List<Book> bookLessBorrower)
        {
            MostBorrowedQty = mostBorrowedQty;
            BookMostBorrowed = bookMostBorrowed;
            LessBorrowedQty = lessBorrowedQty;
            BookLessBorrower = bookLessBorrower;
        }
        public BooksMostAndLessBorrow()
        {

        }
    }
}
