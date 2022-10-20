using LibraryApi.Models;
using LibraryApi.Models.Books;
using LibraryApi.Models.Borrows;
using LibraryApi.Models.Customers;
using LibraryApi.Services.Interfaces;
using LibraryApi.Utilities;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class CustomerService : ICustomerService
    {
        private string _filePath = "./Files/Customers.txt";
        private string _filePathBorrow = "./Files/Borrow.txt";
        private IWriterReader _dal;

        public CustomerService (IWriterReader dal)
        {
            _dal = dal;
        }

        public void AddCustomer(Customer newCustomer)
        {
            List<Customer> list = _dal.Read<Customer>(_filePath);
            int countId = ExtensionMethodIdentityId.MaxCustomerIdValue(list);
            newCustomer.Id = countId;
            _dal.Write(JsonSerializer.Serialize(newCustomer), _filePath);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customerList = _dal.Read<Customer>(_filePath);
            return customerList;
        }

        public Customer GetCustomerById(int customerId)
        {
            List<Customer> list = _dal.Read<Customer>(_filePath);
            var customer = list.FirstOrDefault(customer => customer.Id == customerId);
            return customer;
        }

        public List<Customer> GetMostCustomerWithBorrow()
        {
            List<Customer> listCustomer = _dal.Read<Customer>(_filePath);
            List<Borrow> listBorrow = _dal.Read<Borrow>(_filePathBorrow);
            var listMostBorrowed = new List<Book>();
            List<CustomerIdQuantityBorrow> listCustomersAndQty = new List<CustomerIdQuantityBorrow>();
            foreach (var customer in listCustomer)
            {
                var newCustomerRecord = new CustomerIdQuantityBorrow(customer.Id, 0);
                listCustomersAndQty.Add(newCustomerRecord);
            }
            foreach (var borrow in listBorrow)
            {
                foreach (var customerAndQty in listCustomersAndQty)
                {
                    if (borrow.CustomerId == customerAndQty.Id)
                    {
                        customerAndQty.Quantity++;
                    }
                }
            }

            List<CustomerIdQuantityBorrow> orderedList = listCustomersAndQty.OrderBy(record => record.Quantity).ToList();
            CustomerIdQuantityBorrow mostQty = orderedList[orderedList.Count() - 1];
            int mostQtyItem = mostQty.Quantity;
            List<CustomerIdQuantityBorrow> listMostQty = new List<CustomerIdQuantityBorrow>();

            foreach (var record in orderedList)
            {
                if (record.Quantity == mostQty.Quantity)
                {
                    listMostQty.Add(record);
                }

            }
            var listMostSelected = listMostQty.Select(item => item.Id);
            List<Customer> listToAddForMost = new List<Customer>();
            foreach (var customer in listCustomer)
            {
                foreach (var record in listMostSelected)
                {
                    if (customer.Id == record)
                    {
                        listToAddForMost.Add(customer);
                    }
                }
            }


            return listToAddForMost;
        }

        public void DeleteCustomer(int customerId)
        {
            List<Customer> customerList = _dal.Read<Customer>(_filePath);
            File.WriteAllText(_filePath, string.Empty);

            foreach (Customer item in customerList)
            {
                if (item.Id != customerId)
                {
                    _dal.Write(JsonSerializer.Serialize(item), _filePath);
                }
            }
        }

        public Customer UpdateCustomer(int customerId, Customer customer)
        {
            List<Customer> list = _dal.Read<Customer>(_filePath);

            int countId = list.MaxCustomerIdValue();

            var customerToUpdate = list.FirstOrDefault(customer => customer.Id == customerId);


            if (customerToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Customer item in list)
                {
                    if (item.Id != customerToUpdate.Id)
                    {
                        _dal.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        customer.Id = customerToUpdate.Id;
                        _dal.Write(JsonSerializer.Serialize(customer), _filePath);
                    }
                }

                return null;
            }
            else
            {
                customer.Id = countId;
                _dal.Write(JsonSerializer.Serialize(customer), _filePath);
                return customer;
            }
        }
    }
}
