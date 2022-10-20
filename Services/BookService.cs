using LibraryApi.Models;
using LibraryApi.Models.Books;
using LibraryApi.Models.Borrows;
using LibraryApi.Services.Interfaces;
using LibraryApi.Utilities;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class BookService : IBookService
    {
        private string _filePath = "./Files/Books.txt";
        private string _filePathBorrow = "./Files/Borrow.txt";
        private IWriterReader _dal;

        public BookService(IWriterReader dal)
        {
            _dal = dal;
        }


        public void AddBook(Book newBook)
        {
            List<Book> list = _dal.Read<Book>(_filePath);
            int countId = ExtensionMethodIdentityId.MaxBookIdValue(list);
            newBook.Id = countId;
            _dal.Write(JsonSerializer.Serialize(newBook), _filePath);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var bookList = _dal.Read<Book>(_filePath);
            return bookList;
        }

        public BookDetails GetBookById(int bookId)
        {
            List<Book> list = _dal.Read<Book>(_filePath);
            List<Borrow> borrowList = _dal.Read<Borrow>(_filePathBorrow);
            var book = list.FirstOrDefault(book => book.Id == bookId);
            var borrow = new Borrow();
            
            BookDetails bookDetails = new BookDetails();
            if(book != null)
            {
                borrow = borrowList.FirstOrDefault(borrow => borrow.BookId == book.Id);
                bookDetails.Id = book.Id;
                bookDetails.Title = book.Title;
                bookDetails.Author = book.Author;
                bookDetails.Genre = book.Genre;
                if (borrow != null)
                {
                    if (borrow.BorrowEnd == null)
                    {
                        bookDetails.ActualBorrow = borrow;
                    }
                }

            }
            else {
                bookDetails = null;
            }
           
            
            
            return bookDetails;
        }

        public BooksMostAndLessBorrow GetBookMostAndLessBorrowed()
        {
            List<Book> listBook = _dal.Read<Book>(_filePath);
            List<Borrow> listBorrow = _dal.Read<Borrow>(_filePathBorrow);
            var listMostBorrowed = new List<Book>();
            var listLessBorrowed = new List<Book>();
            List<BookIdQuantityBorrow> listBooksAndQty = new List<BookIdQuantityBorrow>();
            foreach (var book in listBook)
            {
                var newBookRecord = new BookIdQuantityBorrow(book.Id, 0);
                listBooksAndQty.Add(newBookRecord);
            }
            foreach (var borrow in listBorrow)
            {
                foreach (var bookAndQty in listBooksAndQty)
                {
                    if (borrow.BookId == bookAndQty.Id)
                    {
                        bookAndQty.Quantity++;
                    }
                }
            }

            List<BookIdQuantityBorrow> orderedList = listBooksAndQty.OrderBy(record => record.Quantity).ToList();
            BookIdQuantityBorrow lessQty = orderedList[0];
            BookIdQuantityBorrow mostQty = orderedList[orderedList.Count() -1];
            int mostQtyItem = mostQty.Quantity;
            int LessQtyItem = lessQty.Quantity;
            List<BookIdQuantityBorrow> listLessQty = new List<BookIdQuantityBorrow>();
            List<BookIdQuantityBorrow> listMostQty = new List<BookIdQuantityBorrow>();

            foreach(var record in orderedList)
            {
                if(record.Quantity == mostQty.Quantity)
                {
                    listMostQty.Add(record);
                }
                if (record.Quantity == lessQty.Quantity)
                {
                    listLessQty.Add(record);
                }

            }
            var listLessSelected = listLessQty.Select(item => item.Id);
            var listMostSelected = listMostQty.Select(item => item.Id);
            List<Book> listToAddForMost = new List<Book>();
            List<Book> listToAddForLess = new List<Book>();
            foreach(var book in listBook)
            {
                foreach(var record in listMostSelected)
                {
                    if (book.Id == record)
                    {
                        listToAddForMost.Add(book);
                    }
                }
                foreach (var record in listLessSelected)
                {
                    if (book.Id == record)
                    {
                        listToAddForLess.Add(book);
                    }
                }
            }

            BooksMostAndLessBorrow objectToReturn = new BooksMostAndLessBorrow(mostQtyItem, 
                                                                                listToAddForMost, 
                                                                                LessQtyItem, 
                                                                                listToAddForLess);

            return objectToReturn;

        }

        public List<Book> GetMostBorrowedBooks()
        {
            List<Book> listBook = _dal.Read<Book>(_filePath);
            List<Borrow> listBorrow = _dal.Read<Borrow>(_filePathBorrow);
            var listMostBorrowed = new List<Book>();
            List<BookIdQuantityBorrow> listBooksAndQty = new List<BookIdQuantityBorrow>();
            foreach (var book in listBook)
            {
                var newBookRecord = new BookIdQuantityBorrow(book.Id, 0);
                listBooksAndQty.Add(newBookRecord);
            }
            foreach (var borrow in listBorrow)
            {
                foreach (var bookAndQty in listBooksAndQty)
                {
                    if (borrow.BookId == bookAndQty.Id)
                    {
                        bookAndQty.Quantity++;
                    }
                }
            }

            List<BookIdQuantityBorrow> orderedList = listBooksAndQty.OrderBy(record => record.Quantity).ToList();
            BookIdQuantityBorrow mostQty = orderedList[orderedList.Count() - 1];
            int mostQtyItem = mostQty.Quantity;
            List<BookIdQuantityBorrow> listMostQty = new List<BookIdQuantityBorrow>();

            foreach (var record in orderedList)
            {
                if (record.Quantity == mostQty.Quantity)
                {
                    listMostQty.Add(record);
                }

            }
            var listMostSelected = listMostQty.Select(item => item.Id);
            List<Book> listToAddForMost = new List<Book>();
            foreach (var book in listBook)
            {
                foreach (var record in listMostSelected)
                {
                    if (book.Id == record)
                    {
                        listToAddForMost.Add(book);
                    }
                }
            }


            return listToAddForMost;
        }

        public void DeleteBook(int bookId)
        {
            List<Book> bookList = _dal.Read<Book>(_filePath);
            File.WriteAllText(_filePath, string.Empty);

            foreach (Book item in bookList)
            {
                if (item.Id != bookId)
                {
                    _dal.Write(JsonSerializer.Serialize(item), _filePath);
                }
            }
        }

        public Book UpdateBook(int bookId, Book book)
        {
            List<Book> list = _dal.Read<Book>(_filePath);

            int countId = list.MaxBookIdValue();

            var bookToUpdate = list.FirstOrDefault(book => book.Id == bookId);


            if (bookToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Book item in list)
                {
                    if (item.Id != bookToUpdate.Id)
                    {
                        _dal.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        book.Id = bookToUpdate.Id;
                        _dal.Write(JsonSerializer.Serialize(book), _filePath);
                    }
                }

                return null;
            }
            else
            {
                book.Id = countId;
                _dal.Write(JsonSerializer.Serialize(book), _filePath);
                return book;
            }
        }
    }
}
