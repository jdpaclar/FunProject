using System.Collections.Generic;
using ParcelApp.Common.Constants;

namespace ParcelApp.Common
{
    public class ParcelOrderOutput
    {
        public bool IsSpeedy { get; set; }
        public List<ParcelOrderOutputItem> LineItems { get; set; } = new List<ParcelOrderOutputItem>();
        public decimal TotalCost { get; set; }
        public decimal TotalSaved { get; set; }
        public List<DiscountTypes> AppliedDiscountTypes { get; set; }
    }

    public class ParcelOrderOutputItem
    {
        public decimal Cost { get; set; }
        public string ParcelType { get; set; }
        public DiscountTypes DiscountTypes { get; set; }
    }
}