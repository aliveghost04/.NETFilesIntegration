using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FilesIntegration.Utils
{
    public static class CsvReader
    {
        public static List<string> GetListFromStream(Stream stream, int? skip = null)
        {
            var values = new List<string>();
            try
            {
                
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader reader = new StreamReader(stream))
                {
                   
                    string line;
                    int count = 0;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = reader.ReadLine()) != null)
                    {
                        count++;
                        line = line.Trim();//.Replace(" ", "");
                        if(skip == null || count > skip)
                            values.Add(line);
                    }
                }

            }catch (Exception) { }

            return values;
        }
    }
}