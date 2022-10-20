using LibraryApi.Models.Books;
using LibraryApi.Models.Borrows;
using LibraryApi.Models.Customers;

namespace LibraryApi.Utilities
{
    public static class ExtensionMethodIdentityId
    {
        public static int MaxBookIdValue(this IEnumerable<Book> enumerable)
        {
            int countId = 10000;
            foreach (var item in enumerable)
            {
                if (item.Id >= countId)
                {
                    countId = item.Id + 1;
                }
            }
            return countId;
        }

        public static int MaxCustomerIdValue(this IEnumerable<Customer> enumerable)
        {
            int countId = 30000;
            foreach (var item in enumerable)
            {
                if (item.Id >= countId)
                {
                    countId = item.Id + 1;
                }
            }
            return countId;
        }

        public static int MaxBorrowIdValue(this IEnumerable<Borrow> enumerable)
        {
            int countId = 50000;
            foreach (var item in enumerable)
            {
                if (item.Id >= countId)
                {
                    countId = item.Id + 1;
                }
            }
            return countId;
        }
    }
}

