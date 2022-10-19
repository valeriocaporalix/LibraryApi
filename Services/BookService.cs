using LibraryApi.Models;
using LibraryApi.Utilities;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class BookService
    {
        private string _filePath = "./Files/Books.txt";

        public void AddBook(Book newBook)
        {
            List<Book> list = WriterReader.Read<Book>(_filePath);
            int countId = ExtensionMethodIdentityId.MaxBookIdValue(list);
            newBook.Id = countId;
            WriterReader.Write(JsonSerializer.Serialize(newBook), _filePath);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var bookList = WriterReader.Read<Book>(_filePath);
            return bookList;
        }

        public Book GetBookById(int bookId)
        {
            List<Book> list = WriterReader.Read<Book>(_filePath);
            var book = list.FirstOrDefault(book => book.Id == bookId);
            return book;
        }

        public void DeleteBook(int bookId)
        {
            List<Book> bookList = WriterReader.Read<Book>(_filePath);
            File.WriteAllText(_filePath, string.Empty);

            foreach (Book item in bookList)
            {
                if (item.Id != bookId)
                {
                    WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                }
            }
        }

        public Book UpdateBook(int bookId, Book book)
        {
            List<Book> list = WriterReader.Read<Book>(_filePath);

            int countId = list.MaxBookIdValue();

            var bookToUpdate = list.FirstOrDefault(book => book.Id == bookId);


            if (bookToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Book item in list)
                {
                    if (item.Id != bookToUpdate.Id)
                    {
                        WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        book.Id = bookToUpdate.Id;
                        WriterReader.Write(JsonSerializer.Serialize(book), _filePath);
                    }
                }

                return null;
            }
            else
            {
                book.Id = countId;
                WriterReader.Write(JsonSerializer.Serialize(book), _filePath);
                return book;
            }
        }
    }
}
