using System;
using static DataModels.Enums;

namespace DataModels
{
    public class ScannerResult
    {
        public string Barcode { get; set; }
        public EHaspelStatus Status { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
    }

    public class Enums
    {
        public enum EHaspelStatus
        {
            Unkown,
            Full,
            IsUsed,
            Empty
        }
    }
}
