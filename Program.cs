using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
namespace readMultipleFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile readFile = new ReadFile();
            string[] files = { "1.txt", "2.txt" };
            readFile.ReadFileAsync(files).GetAwaiter().GetResult();
        }
    }

       
}
