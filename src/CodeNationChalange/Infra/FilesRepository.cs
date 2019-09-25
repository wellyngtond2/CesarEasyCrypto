using CodeNationChalange.Models;
using Newtonsoft.Json;
using System.IO;

namespace CodeNationChalange.Infra
{
    public class FilesRepository
    {
        private static string path { get { return Directory.GetCurrentDirectory(); } }

        public static void SaveFile(ResponseObject json)
        {
            File.WriteAllText(Path.Combine(path, "answer.json"), JsonConvert.SerializeObject(json));
        }

        public static ResponseObject OpenFile()
        {
            using (StreamReader file = new StreamReader(Path.Combine(path, "answer.json")))
            {
                return JsonConvert.DeserializeObject<ResponseObject>(file.ReadToEnd());
            }
        }

        public static byte[] GetFileString(string fileName)
        {
             return ConverteStreamToByteArray2(new FileStream(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read));
        }

        private static byte[] ConverteStreamToByteArray2(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
