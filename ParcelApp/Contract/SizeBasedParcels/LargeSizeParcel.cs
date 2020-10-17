using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class LargeSizeParcel: ISizeParcel
    {
        public double Min { get; } = 50;
        public double Max { get; } = 99;
        public decimal Cost { get; } = 15m;
        public ParcelTypes ParcelType { get; } = ParcelTypes.Large;
        public double WeightLimit { get; } = 6;
    }
}