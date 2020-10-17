using System.Collections.Generic;

namespace ParcelApp.Common
{
    public class ParcelOrder
    {
        public bool Speedy { get; set; }

        public List<ParcelOrderItem> ParcelOrderItems { get; set; }
    }

    public class ParcelOrderItem
    {
        public ParcelOrderItem(double size, double weight, bool calculateBySize = false)
        {
            Size = size;
            Weight = weight;
            CalculateBySize = calculateBySize;
        }
        
        public double Size { get; }
        public double Weight { get; }
        public bool CalculateBySize { get; }
    }
}