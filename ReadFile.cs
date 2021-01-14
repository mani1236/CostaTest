using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace readMultipleFiles
{
   public class ReadFile
    {

        public  async Task ReadFileAsync(string[] filePaths)
        {
            IList<Task<string>> readTaskList = new List<Task<string>>();
            for (int index = 0; index < filePaths.Length; ++index)
            {
                // building Aynchronous tasks which internally rely on multithreading
                readTaskList.Add(File.ReadAllTextAsync(filePaths[index]));
            }
            // Await / Run in paraller when all the Asynchronous tasks are completed and
            // get result of file text in String array
            string[] finishedTask = await Task.WhenAll(readTaskList);
            List<Driver> drivers = new List<Driver>();
            // Iterating through result of tasks
            for (int j = 0; j < finishedTask.Length; ++j)
            {
                using (StringReader reader = new StringReader(finishedTask[j]))
                {
                    string line;
                    Boolean isFirstLine = true;
                    int orderPos = 0;
                    int namePos = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // First get the position of keys "Name" and "Order" and 
                        // and make a note of them
                        if (isFirstLine && line != string.Empty)
                        {
                            isFirstLine = false;
                            String[] vals = line.Split(',');
                            for (int i = 0; i < vals.Length; ++i)
                            {
                                if (vals[i].Trim() == "Name")
                                {
                                    namePos = i;
                                }
                                else if (vals[i].Trim() == "Order")
                                {
                                    orderPos = i;
                                }
                            }
                        }
                        else
                        {
                            // Do not process if String is empty
                            if (line != string.Empty)
                            {
                                // Find the values of respective positions (string index) and add to drivers list 
                                String[] vals = line.Split(',');
                                drivers.Add(new Driver() { Name = vals[namePos].Trim(), Order = Convert.ToInt32(vals[orderPos]) });
                            }
                        }
                    }
                }

            }
            // Sorting the driver list objects by comparing Order property
            drivers.Sort((x, y) => x.Order.CompareTo(y.Order));
            Console.WriteLine("Name, Order");
            foreach (Driver item in drivers)
            {
                Console.WriteLine(item.Order + ", " + item.Name);
            }
        }
    }

    class Driver
    {
        public int Order { get; set; }
        public string Name { get; set; }
    }

}

