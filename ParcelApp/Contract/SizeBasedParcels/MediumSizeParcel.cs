using ParcelApp.Common;
using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class MediumSizeParcel : ISizeParcel
    {
        public double Min { get; } = 10;
        public double Max { get; } = 49;
        public decimal Cost { get; } = 8m;
        
        public string ParcelType { get; } = ParcelTypes.Medium.ToString();
        public double WeightLimit { get; } = 3;
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.Medium;
    }
}