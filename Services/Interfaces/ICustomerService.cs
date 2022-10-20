using LibraryApi.Models.Customers;

namespace LibraryApi.Services.Interfaces
{
    public interface ICustomerService
    {
        public void AddCustomer(Customer newCustomer);

        public IEnumerable<Customer> GetAllCustomers();

        public Customer GetCustomerById(int customerId);

        public List<Customer> GetMostCustomerWithBorrow();

        public void DeleteCustomer(int customerId);

        public Customer UpdateCustomer(int customerId, Customer customer);

    }
}
