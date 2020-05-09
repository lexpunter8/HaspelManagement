using AllinqManagementApi.Adapters;
using AllinqManagementApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AllinqManagementApi.Services
{
    public class TeamService : IFileService<string>
    {
        private IFileAdapter<string> myTeamAdpater;
        private List<string> myTeams = new List<string>();

        private bool isUpToDate;
        private EventHandler TeamsChanged;
        public TeamService(IFileAdapter<string> fileAdapter)
        {
            myTeamAdpater = fileAdapter;
            myTeams = myTeamAdpater.GetData().ToList();
            isUpToDate = true;

            TeamsChanged += async (s, e) => {
                await myTeamAdpater.WriteData(myTeams.ToArray());
                isUpToDate = false;
            };
        } 
        public string[] GetAllData()
        {
            if (!isUpToDate)
            {
                myTeams = myTeamAdpater.GetData().ToList();
            }
            return myTeams.ToArray();
        }

        public string GetByBarcode(string barcode)
        {
            throw new NotImplementedException();
        }

        public void InsertData(string[] data)
        {
            myTeams.AddRange(data);
            TeamsChanged?.Invoke(this, new EventArgs());
        }

        public void Remove(string data)
        {
            myTeams.Remove(data);
            TeamsChanged?.Invoke(this, new EventArgs());
        }

        public void Update(string data)
        {
            myTeams.Add(data);
            TeamsChanged?.Invoke(this, new EventArgs());
        }
    }
}
