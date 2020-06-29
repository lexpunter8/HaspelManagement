using System;
using System.Collections.Generic;
using System.Linq;
using AllinqManagementApi.Adapters;
using AllinqManagementApi.Interfaces;
using DataModels;

namespace AllinqManagementApi.Services
{
    public class CsvFileService : IFileService<Haspel>
    {
        private IFileAdapter<Haspel> myHaspelFileAdapter;
        private List<Haspel> myHaspels = new List<Haspel>();

        private EventHandler HaspelsChanged;

        public CsvFileService(IFileAdapter<Haspel> fileAdapter)
        {
            myHaspelFileAdapter = fileAdapter;

            UpdateLocalData();

            HaspelsChanged += async (s, e) => await myHaspelFileAdapter.WriteData(myHaspels.ToArray()).ContinueWith(_ => UpdateLocalData());
        }

        private void UpdateLocalData()
        {
            myHaspels.Clear();

            myHaspels.AddRange(myHaspelFileAdapter.GetData());
        }

        public Haspel[] GetAllData()
        {
            return myHaspels.ToArray();
        }

        public Haspel GetByBarcode(string barcode)
        {
            return myHaspels.FirstOrDefault(h => h.Barcode.Equals(barcode));
        }

        public void Update(Haspel data)
        {
            if (data == null)
            {
                return;
            }
            if (myHaspels == null)
            {
                Console.Write("");
            }
            var existingHaspel = myHaspels.FirstOrDefault(h => h.Barcode == data.Barcode);

            if (existingHaspel == null)
            {
                myHaspels.Add(data);
                HaspelsChanged?.Invoke(this, new EventArgs());
                return;
            }

            existingHaspel.Comment = data.Comment;
            existingHaspel.Status = data.Status;
            existingHaspel.UsedBy = data.UsedBy;

            HaspelsChanged?.Invoke(this, new EventArgs());
        }

        public void InsertData(Haspel[] data)
        {
            myHaspels.AddRange(data);
            HaspelsChanged?.Invoke(this, new EventArgs());
        }

        public void Remove(Haspel data)
        {
            myHaspels.Remove(data);
            HaspelsChanged?.Invoke(this, new EventArgs());
        }
    }
}
