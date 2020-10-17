using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class MediumSizeParcel : ISizeParcel
    {
        public double Min { get; } = 10;
        public double Max { get; } = 49;
        public decimal Cost { get; } = 8m;
        
        public ParcelTypes ParcelType { get; } = ParcelTypes.Medium;
        public double WeightLimit { get; } = 3;
    }
}