using LibraryApi.Models;
using LibraryApi.Models.Books;
using LibraryApi.Models.Borrows;
using LibraryApi.Models.Customers;
using LibraryApi.Services.Interfaces;
using LibraryApi.Utilities;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class BorrowService : IBorrowService
    {
        private string _filePath = "./Files/Borrow.txt";
        private string _filePathBook = "./Files/Books.txt";
        private string _filePathCustomer = "./Files/Customers.txt";
        private IWriterReader _dal;

        public BorrowService(IWriterReader dal)
        {
            _dal = dal;
        }


        public Borrow AddBorrow(Borrow newBorrow)
        {
            List<Borrow> list = _dal.Read<Borrow>(_filePath);
            int countId = ExtensionMethodIdentityId.MaxBorrowIdValue(list);
            var listBorrowNotAvailable = list.Where(borrow => borrow.BorrowEnd == null);
            foreach (var borrow in listBorrowNotAvailable)
            {
                if (borrow.BookId == newBorrow.BookId)
                {
                    return null;
                }
            }
            newBorrow.Id = countId;
            _dal.Write(JsonSerializer.Serialize(newBorrow), _filePath);
            return newBorrow;
        }

        public IEnumerable<Borrow> GetAllBorrow()
        {
            var borrowList = _dal.Read<Borrow>(_filePath);
            return borrowList;
        }

        public IEnumerable<Borrow> GetAllBorrowActive()
        {
            List<Borrow> list = _dal.Read<Borrow>(_filePath);
            var borrowActive = list.Where(borrow => borrow.BorrowEnd == null);

            return borrowActive;
        }

        public IEnumerable<Borrow> GetAllBorrowByCustomerId(int customerId)
        {
            List<Customer> listCustomer = _dal.Read<Customer>(_filePathCustomer);
            List<Borrow> listBorrow = _dal.Read<Borrow>(_filePath);
            var customer = listCustomer.FirstOrDefault(customer => customer.Id == customerId);
            List<Borrow> borrows = new List<Borrow>();
            if (customer != null)
            {
                borrows = listBorrow.Where(borrow => borrow.CustomerId == customer.Id).ToList();
            }
            else
            {
                return null;
            }
            return borrows;
        }

        public IEnumerable<Borrow> GetAllBorrowByBookId(int bookId)
        {
            List<Book> listBook = _dal.Read<Book>(_filePathBook);
            List<Borrow> listBorrow = _dal.Read<Borrow>(_filePath);
            var book = listBook.FirstOrDefault(book => book.Id == bookId);
            List<Borrow> borrows = new List<Borrow>();
            if (book != null)
            {
                borrows = listBorrow.Where(borrow => borrow.BookId == book.Id).ToList();
            }
            else
            {
                return null;
            }
            return borrows;
        }

        public BorrowDetails GetBorrowById(int borrowId)
        {
            List<Borrow> list = _dal.Read<Borrow>(_filePath);
            List<Customer> listCustomer = _dal.Read<Customer>(_filePathCustomer);
            List<Book> listBook = _dal.Read<Book>(_filePathBook);
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

        public IEnumerable<Borrow> GetAllBorrowInRange(DateTime startDate, DateTime endDate)
        {
            List<Borrow> borrowList = _dal.Read<Borrow>(_filePath);
            List<Borrow> newListToReturn = new List<Borrow>();
            foreach (var borrow in borrowList)
            {
                if(borrow.BorrowStart >= startDate && borrow.BorrowEnd <= endDate)
                {
                    newListToReturn.Add(borrow);
                }
            }
            return newListToReturn;
        }

        public void DeleteBorrow(int borrowId)
        {
            List<Borrow> borrowList = _dal.Read<Borrow>(_filePath);
            File.WriteAllText(_filePath, string.Empty);

            foreach (Borrow item in borrowList)
            {
                if (item.Id != borrowId)
                {
                    _dal.Write(JsonSerializer.Serialize(item), _filePath);
                }
            }
        }

        public Borrow UpdateBorrow(int borrowId, Borrow borrow)
        {
            List<Borrow> list = _dal.Read<Borrow>(_filePath);

            int countId = list.MaxBorrowIdValue();

            var borrowToUpdate = list.FirstOrDefault(borrow => borrow.Id == borrowId);


            if (borrowToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Borrow item in list)
                {
                    if (item.Id != borrowToUpdate.Id)
                    {
                        _dal.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        borrow.Id = borrowToUpdate.Id;
                        _dal.Write(JsonSerializer.Serialize(borrow), _filePath);
                    }
                }

                return null;
            }
            else
            {
                borrow.Id = countId;
                _dal.Write(JsonSerializer.Serialize(borrow), _filePath);
                return borrow;
            }
        }

        public Borrow UpdateEndBorrow(int borrowId, DateTime input)
        {
            List<Borrow> list = _dal.Read<Borrow>(_filePath);

            var borrowToUpdate = list.FirstOrDefault(borrow => borrow.Id == borrowId);


            if (borrowToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Borrow item in list)
                {
                    if (item.Id != borrowToUpdate.Id)
                    {
                        _dal.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        borrowToUpdate.BorrowEnd = input;
                        _dal.Write(JsonSerializer.Serialize(borrowToUpdate), _filePath);
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
