using System.Security.Cryptography;
using System.Text;

namespace Lab_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string hashPath = "f0.txt";
            string hash = Hash(hashPath);

            using (FileStream fstream = new FileStream(hashPath, FileMode.Append))
            {
                byte[] buffer =
                    Encoding.Default.GetBytes("\n" + hash);

                fstream.Write(buffer, 0, buffer.Length);
            }

            string inputHashPath = "f0.txt";
            string inputHash = HashFile(inputHashPath);

            string[] lines=File.ReadAllLines(inputHashPath);
            string oldHash = lines[lines.Length - 1];

            if(oldHash == inputHash)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }

        static string Hash(string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash)
                        .Replace("-", "").ToLower();
                }
            }
        }

        static string HashFile(string path)
        {
            using (var md5 = MD5.Create())
            {
                string[] stream = File.ReadAllLines(path);
                string[] newStream = new string[stream.Length - 1];
                for (int i = 0; i < newStream.Length; i++)
                {
                    newStream[i] = stream[i];
                }

                byte[] byt = new byte[newStream.Length];
                for (int i = 0; i < newStream.Length; i++)
                { 
                    byt[i] = Convert.ToByte(newStream[i]); 
                }

                var hash = md5.ComputeHash(byt);
                return BitConverter.ToString(hash)
                        .Replace("-", "").ToLower();
            }
        }
    }
}
