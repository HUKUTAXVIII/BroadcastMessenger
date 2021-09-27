using System;
using System.IO;
using ClientServerLib;

namespace ClientConsole
{
    class Program
    {
        static void FileCreateor(string content) {
            
                File.Create(content.Split('-')[2]).Close();
                File.WriteAllText(content.Split('-')[2], content.Remove(0, content.Split('-')[1].Length + content.Split('-')[2].Length+3));
            
        }
        static void Main(string[] args)
        {
            Client client = new Client("127.0.0.1",8000);
            client.Connect();

            try
            {
                while (true) {
                    var data = client.Get();

                    string checker = Client.FromBytesToString(data);
                    if (checker.Split('-').Length >= 2)
                    {
                        if (checker.Split('-')[1] == "text")
                        {
                            FileCreateor(checker);
                            Console.WriteLine("File loaded!");
                            continue;
                        }
                    }

                    Console.WriteLine(DateTime.Now.ToShortTimeString()+":"+Client.FromBytesToString(data));
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            client.Close();
        }
    }
}
