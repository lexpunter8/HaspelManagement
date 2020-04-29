using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DataModels;
using static DataModels.Enums;

namespace AllinqManagementApi.Adapters
{
    public class CsvFileAdapter : IFileAdapter<Haspel>
    {
        private string myFile = Path.Combine(Environment.CurrentDirectory, "Haspels.csv");

        public Haspel[] GetData()
        {
            try
            {
                using (FileStream fs = File.OpenRead(myFile))
                {
                    using (StreamReader streamWriter = new StreamReader(fs))
                    {
                        streamWriter.ReadLine();
                        List<Haspel> haspels = new List<Haspel>();
                        string line;
                        while ((line = streamWriter.ReadLine()) != null)
                        {
                            try
                            {
                                var values = line.Split(',');
                                haspels.Add(new Haspel
                                {
                                    Barcode = values[0] ?? "",
                                    UsedBy = values[1] ?? "",
                                    Status = ConvertStringToStatus(values[2]),
                                    Comment = values[3] ?? ""
                                });

                            }
                            catch (Exception e)
                            {
                                // skip
                            }
                        }

                        return haspels.ToArray();
                    }

                }
            }
            catch (Exception e)
            {
                return new Haspel[0];
            }
        }

        private EHaspelStatus ConvertStringToStatus(string v)
        {
            switch (v.ToLower())
            {
                case "isused":
                    return EHaspelStatus.IsUsed;
                case "isnotused":
                    return EHaspelStatus.Unkown;
                default:
                    return EHaspelStatus.Unkown;
            }
        }

        public async Task WriteData(Haspel[] data)
        {
            await Task.Run(() =>
            {
                using (FileStream fs = File.OpenWrite(myFile))
                {

                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine($"Barcode,In gebruik door, In gebruik, Opmerking");
                        foreach (Haspel h in data)
                        {
                            sw.WriteLine(h.ToCsvString());
                        }
                    }
                }
            });
        }
    }
    public interface IFileAdapter<T>
    {
        T[] GetData();
        Task WriteData(T[] data);
    }
}
