using System.Collections.Generic;

namespace ParcelApp.Common
{
    public class ParcelOrderOutput
    {
        public List<ParcelOrderOutputItem> LineItems { get; set; } = new List<ParcelOrderOutputItem>();
        public decimal TotalCost { get; set; }
    }

    public class ParcelOrderOutputItem
    {
        public decimal Cost { get; set; }
        public string ParcelType { get; set; }
    }
}