using System.Collections.Generic;
using ParcelApp.Common;

namespace ParcelApp.Contract
{
    public class ParcelOutputDisplay
    {
        public string Speedy { get; set; }
        
        public List<ParcelOutputLineDisplay> LineItems { get; set; }
        public string SavedCost { get; set; }
        public string TotalCost { get; set; }
    }

    public class ParcelOutputLineDisplay
    {
        public string Cost { get; set; }
        public string ParcelType { get; set; }
    }
}