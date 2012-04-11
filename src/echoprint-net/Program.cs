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

            using (NCodegen codegen = new NCodegen(@"C:\dev\scratchpad\codegen\ENMFP", "codegen.windows.exe", 10, 30))
            {
                codegen.Start((data) =>
                {
                    if (String.IsNullOrEmpty(data.error))
                    {
                        Console.WriteLine("{0:HH:mm:ss} - {1} {2} {3}", DateTime.Now, data.metadata.title, data.metadata.filename, data.code);
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
