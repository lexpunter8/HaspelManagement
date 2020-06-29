using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
            [System.ComponentModel.Description("Onbekend")]
            Unkown = 0,
            [System.ComponentModel.Description("Vol")]
            Full = 1,
            [System.ComponentModel.Description("In gebruik")]
            IsUsed = 2,
            [System.ComponentModel.Description("Leeg")]
            Empty = 3
        }
    }

    public static class EnumHelpers
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
