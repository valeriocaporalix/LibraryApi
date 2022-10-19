using LibraryApi.Models;
using LibraryApi.Utilities;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class CustomerService
    {
        private string _filePath = "./Files/Customers.txt";

        public void AddCustomer(Customer newCustomer)
        {
            List<Customer> list = WriterReader.Read<Customer>(_filePath);
            int countId = ExtensionMethodIdentityId.MaxCustomerIdValue(list);
            newCustomer.Id = countId;
            WriterReader.Write(JsonSerializer.Serialize(newCustomer), _filePath);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customerList = WriterReader.Read<Customer>(_filePath);
            return customerList;
        }

        public Customer GetCustomerById(int customerId)
        {
            List<Customer> list = WriterReader.Read<Customer>(_filePath);
            var customer = list.FirstOrDefault(customer => customer.Id == customerId);
            return customer;
        }

        public void DeleteCustomer(int customerId)
        {
            List<Customer> customerList = WriterReader.Read<Customer>(_filePath);
            File.WriteAllText(_filePath, string.Empty);

            foreach (Customer item in customerList)
            {
                if (item.Id != customerId)
                {
                    WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                }
            }
        }

        public Customer UpdateCustomer(int customerId, Customer customer)
        {
            List<Customer> list = WriterReader.Read<Customer>(_filePath);

            int countId = list.MaxCustomerIdValue();

            var customerToUpdate = list.FirstOrDefault(customer => customer.Id == customerId);


            if (customerToUpdate != null)
            {
                File.WriteAllText(_filePath, string.Empty);
                foreach (Customer item in list)
                {
                    if (item.Id != customerToUpdate.Id)
                    {
                        WriterReader.Write(JsonSerializer.Serialize(item), _filePath);
                    }
                    else
                    {
                        customer.Id = customerToUpdate.Id;
                        WriterReader.Write(JsonSerializer.Serialize(customer), _filePath);
                    }
                }

                return null;
            }
            else
            {
                customer.Id = countId;
                WriterReader.Write(JsonSerializer.Serialize(customer), _filePath);
                return customer;
            }
        }
    }
}
