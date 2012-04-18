using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using echoprint_net;
using echoprint_net.Data;

namespace LibraryDeduplicator
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(@".\samplefiles");

            var api = new EchonestAPI();
            using (NCodegen codegen = new NCodegen(@"C:\dev\scratchpad\codegen", "codegen.exe", 10, 30))
            {
                codegen.Start((data) =>
                {
                    if (!String.IsNullOrEmpty(data.error))
                    {
                        Console.WriteLine("{0:HH:mm:ss} - {1}", DateTime.Now, data.error);
                        return;
                    }

                    string error;
                    var result = api.IdentifySong(data, out error);
                    PrintStatus(data, result, error);
                });

                foreach (var item in files)
                {
                    Console.WriteLine("{0:HH:mm:ss} - {1}", DateTime.Now, item);
                    codegen.AddFile(item);
                }
            }
        }

        static void PrintStatus(Code data, EchonestResult result, string error)
        {
            if (result == EchonestResult.Success)
            {
                if (!String.IsNullOrEmpty(data.metadata.id))
                    Console.WriteLine("{0:HH:mm:ss} - {3} {1} {2} {4}", DateTime.Now, data.metadata.title, data.metadata.filename, data.metadata.id, data.code);
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
                Console.WriteLine(String.IsNullOrEmpty(error) ? "Which way'd he go?" : error);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
