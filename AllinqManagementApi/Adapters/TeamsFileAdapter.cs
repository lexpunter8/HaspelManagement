using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AllinqManagementApi.Adapters
{
    public class TeamsFileAdapter : IFileAdapter<string>
    {

        private string myFile = Path.Combine(Environment.CurrentDirectory, "C:\\AllinqHaspel\\Teams.csv");
        public string[] GetData()
        {
            try
            {
                using (FileStream fs = File.OpenRead(myFile))
                {
                    using (StreamReader streamWriter = new StreamReader(fs))
                    {
                        List<string> teams = new List<string>();
                        string line;
                        while ((line = streamWriter.ReadLine()) != null)
                        {
                            try
                            {
                                teams.Add(line);
                            }
                            catch (Exception e)
                            {
                                // skip
                            }
                        }

                        return teams.ToArray();
                    }

                }
            }
            catch (Exception e)
            {
                if (!File.Exists(myFile))
                {

                }
                return new string[0];
            }
        }

        public async Task WriteData(string[] data)
        {
            await Task.Run(() =>
            {
                using var fileStream = File.Open(myFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using StreamWriter sw = new StreamWriter(fileStream);
                foreach (string h in data)
                {
                    sw.WriteLine(h);
                }
            });
        }
    }
}
