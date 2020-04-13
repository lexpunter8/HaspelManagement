using System;
namespace AllinqManagementApi.Interfaces
{
    public interface IFileService<T>
    {
        T[] GetAllData();
        T GetByBarcode(string barcode);
        void InsertData(T[] data);
        void Update(T data);
    }
}
