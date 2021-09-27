using System;
using System.IO;
using System.Threading.Tasks;
using ClientServerLib;


namespace BroadcastMessenger
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1",8000);
            Task NewClientsChecker = new Task(() => { while (true) { server.AddClient("127.0.0.1", 8000); } });
            server.Start();
            NewClientsChecker.Start();
            try
            {

                while (true) {

                    if (server.handler.Count != 0) {
                        Console.Write("Send Message(Enter path to txt file to send it):");
                        var message = Console.ReadLine();
                        if (File.Exists(message)) {
                            if (new FileInfo(message).Extension == ".txt")
                            {
                                message = "-text-" + new FileInfo(message).Name + "-" + File.ReadAllText(message);
                            }
                        }
                        for (int i = 0; i < server.handler.Count-1; i++) {
                            server.Send(Server.FromStringToBytes(message), i);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            server.Close();


        }
    }
}
