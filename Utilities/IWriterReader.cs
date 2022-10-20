namespace LibraryApi.Utilities
{
    public interface IWriterReader
    {
        public void Write(string inputText, string filePath);

        public List<T> Read<T>(string filePath);
    }
}
