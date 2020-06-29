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
        private string myFile = Path.Combine(Environment.CurrentDirectory, "C:\\AllinqHaspel\\Haspels.csv");

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
                                var status = values[1].Split('-');
                                haspels.Add(new Haspel
                                {
                                    Barcode = values[0] ?? "",
                                    UsedBy = status.Length > 1 ? status[1].Trim() : string.Empty,
                                    Status = ConvertStringToStatus(status[0].Trim()),
                                    Comment = values[2] ?? ""
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
            if (EHaspelStatus.Empty.GetEnumDescription() == v)
            {
                return EHaspelStatus.Empty;
            }
            if (EHaspelStatus.Full.GetEnumDescription() == v)
            {
                return EHaspelStatus.Full;
            }
            if (v.StartsWith(EHaspelStatus.IsUsed.GetEnumDescription()))
            {
                return EHaspelStatus.IsUsed;
            }
            return EHaspelStatus.Unkown;
        }

        public async Task WriteData(Haspel[] data)
        {
            await Task.Run(() =>
            {
                using var fileStream = File.Open(myFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using StreamWriter sw = new StreamWriter(fileStream);
                sw.WriteLine($"Barcode, Status, Opmerking");
                foreach (Haspel h in data)
                {
                    sw.WriteLine(h.ToCsvString());
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
