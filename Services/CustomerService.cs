using LibraryApi.Models;
using LibraryApi.Utilities;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class CustomerService
    {
        private string filePath = "./Files/Customers.txt";

        public void AddCustomer(Customer newCustomer)
        {
            List<Customer> list = WriterReader.Read<Customer>(filePath);
            int countId = ExtensionMethodIdentityId.MaxCustomerIdValue(list);
            newCustomer.Id = countId;
            WriterReader.Write(JsonSerializer.Serialize(newCustomer), filePath);
        }
    }
}
