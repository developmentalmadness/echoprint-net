using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace echoprint_net
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles("C:\\Users\\Mark\\Music\\Google Music\\Bon Jovi\\Cross Road");

            var api = new EchonestAPI();
            using (NCodegen codegen = new NCodegen(@"C:\dev\scratchpad\codegen\ENMFP", "codegen.windows.exe", 10, 30))
            {
                codegen.Start((data) =>
                {
                    if (String.IsNullOrEmpty(data.error))
                    {
                        string error;
                        var result = api.IdentifySong(data, out error);
                        if (result == EchonestResult.Success)
                        {
                            if (!String.IsNullOrEmpty(data.metadata.id))
                                Console.WriteLine("{0:HH:mm:ss} - {3} {1} {2}", DateTime.Now, data.metadata.title, data.metadata.filename, data.metadata.id);
                            else
                                Console.WriteLine("{0:HH:mm:ss} - NOT FOUND {1} {2}", DateTime.Now, data.metadata.title, data.metadata.filename);
                        }
                        else if (result == EchonestResult.NotFound)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("'{0}' not found.", data.metadata.filename);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(error);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0:HH:mm:ss} - {1} {2}", DateTime.Now, data.error);
                    }
                });

                foreach (var item in files)
                    codegen.AddFile(item);
            }
        }
    }
}
