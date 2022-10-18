using LibraryApi.Models;

namespace LibraryApi.Utilities
{
    public static class ExtensionMethodIdentityId
    {
        public static int MaxBookIdValue(this IEnumerable<Book> enumerable)
        {
            int countId = 0;
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
            int countId = 0;
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
            int countId = 0;
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

