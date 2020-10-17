using System.Collections.Generic;

namespace ParcelApp.Common
{
    public class ParcelOrder
    {
        public bool Speedy { get; set; }
        
        public List<int> ParcelSizes { get; set; }
    }
}