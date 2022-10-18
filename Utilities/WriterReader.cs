using System.Text.Json;

namespace LibraryApi.Models
{
    public static class WriterReader
    {
        public static void Write(string inputText, string filePath)
        {
            using (var streamWriter = File.AppendText(filePath))
            {
                streamWriter.WriteLine(inputText);
            }
        }


        public static List<T> Read<T>(string filePath)
        {
            List<T> list = new List<T>();
            using var streamReader = new StreamReader(filePath);
            while (!streamReader.EndOfStream)
            {
                var singleObject = JsonSerializer.Deserialize<T>(streamReader.ReadLine());
                list.Add(singleObject);
            }

            return list;
        }


    }
}
