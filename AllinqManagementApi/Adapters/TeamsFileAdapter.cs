using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AllinqManagementApi.Adapters
{
    public class TeamsFileAdapter : IFileAdapter<string>
    {

        private string myFile = Path.Combine(Environment.CurrentDirectory, "Teams.csv");
        public string[] GetData()
        {
            try
            {
                using (FileStream fs = File.OpenRead(myFile))
                {
                    using (StreamReader streamWriter = new StreamReader(fs))
                    {
                        streamWriter.ReadLine();
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
                return new string[0];
            }
        }

        public async Task WriteData(string[] data)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = File.OpenWrite(myFile))
                {

                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (string h in data)
                        {
                            sw.WriteLine(h);
                        }
                    }
                }
            });
        }
    }
}
