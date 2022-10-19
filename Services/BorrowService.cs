using LibraryApi.Models;
using LibraryApi.Utilities;
using System.Text.Json;

namespace LibraryApi.Services
{
    public class BorrowService
    {
        private string _filePath = "./Files/Borrow.txt";

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

        public Borrow GetBorrowById(int borrowId)
        {
            List<Borrow> list = WriterReader.Read<Borrow>(_filePath);
            var borrow = list.FirstOrDefault(borrow => borrow.Id == borrowId);
            return borrow;
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
    }
}
