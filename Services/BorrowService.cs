using LibraryApi.Models;
using LibraryApi.Utilities;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class BorrowService
    {
        private string _filePath = "./Files/Borrow.txt";
        private string _filePathBook = "./Files/Books.txt";
        private string _filePathCustomer = "./Files/Customers.txt";

        public void AddBorrow(Borrow newBorrow)
        {
            List<Borrow> list = WriterReader.Read<Borrow>(_filePath);
            int countId = ExtensionMethodIdentityId.MaxBorrowIdValue(list);
            newBorrow.Id = countId;
            WriterReader.Write(JsonSerializer.Serialize(newBorrow), _filePath);
        }

        public IEnumerable<Borrow> GetAllBorrow()
        {
            var borrowList = WriterReader.Read<Borrow>(_filePath);
            return borrowList;
        }

        public IEnumerable<Borrow> GetAllBorrowActive()
        {
            List<Borrow> list = WriterReader.Read<Borrow>(_filePath);
            var borrowActive = list.Where(borrow => borrow.BorrowEnd == null);

            return borrowActive;
        }

        public BorrowDetails GetBorrowById(int borrowId)
        {
            List<Borrow> list = WriterReader.Read<Borrow>(_filePath);
            List<Customer> listCustomer = WriterReader.Read<Customer>(_filePathCustomer);
            List<Book> listBook = WriterReader.Read<Book>(_filePathBook);
            var borrow = list.FirstOrDefault(borrow => borrow.Id == borrowId);
            var customer = new Customer();
            var book = new Book();
            BorrowDetails borrowDetails = new BorrowDetails();
            if (borrow != null)
            {
                borrowDetails.Id = borrow.Id;
                borrowDetails.BorrowStart = borrow.BorrowStart;
                borrowDetails.BorrowEnd = borrow.BorrowEnd;
                customer = listCustomer.FirstOrDefault(customer => customer.Id == borrow.CustomerId);
                book = listBook.FirstOrDefault(book => book.Id == borrow.BookId);
                if(book != null)
                {
                    borrowDetails.Book = book;
                }
                if(customer != null)
                {
                    borrowDetails.Customer = customer;
                }
            }
            else
            {
                borrowDetails = null;
            }
            
            return borrowDetails;
        }

        public void DeleteBorrow(int borrowId)
        {
            List<Borrow> borrowList = WriterReader.Read<Borrow>(_filePath);
            File.WriteAllText(_filePath, string.Empty);

            foreach (Borrow item in borrowList)
            {
                if (item.Id != borrowId)
                {
                    WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                }
            }
        }

        public Borrow UpdateBorrow(int borrowId, Borrow borrow)
        {
            List<Borrow> list = WriterReader.Read<Borrow>(_filePath);

            int countId = list.MaxBorrowIdValue();

            var borrowToUpdate = list.FirstOrDefault(borrow => borrow.Id == borrowId);


            if (borrowToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Borrow item in list)
                {
                    if (item.Id != borrowToUpdate.Id)
                    {
                        WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        borrow.Id = borrowToUpdate.Id;
                        WriterReader.Write(JsonSerializer.Serialize(borrow), _filePath);
                    }
                }

                return null;
            }
            else
            {
                borrow.Id = countId;
                WriterReader.Write(JsonSerializer.Serialize(borrow), _filePath);
                return borrow;
            }
        }

        public Borrow UpdateEndBorrow(int borrowId, DateTime input)
        {
            List<Borrow> list = WriterReader.Read<Borrow>(_filePath);

            var borrowToUpdate = list.FirstOrDefault(borrow => borrow.Id == borrowId);


            if (borrowToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Borrow item in list)
                {
                    if (item.Id != borrowToUpdate.Id)
                    {
                        WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        borrowToUpdate.BorrowEnd = input;
                        WriterReader.Write(JsonSerializer.Serialize(borrowToUpdate), _filePath);
                    }
                }

                return borrowToUpdate;
            }
            else
            {
                return null;
            }
        }
    }
}
