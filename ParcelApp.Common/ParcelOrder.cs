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
        public ParcelOrderItem(double size, double weight, CalculationType calculationType)
        {
            Size = size;
            Weight = weight;
            CalculationType = calculationType;
        }
        
        public double Size { get; }
        public double Weight { get; }
        public CalculationType CalculationType { get; }
    }

    public enum CalculationType
    {
        BySize,
        ByWeight
    }
}