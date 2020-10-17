using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class ExtraLargeSizeParcel: ISizeParcel
    {
        public double Min { get; } = 100;
        public double Max { get; } = double.MaxValue;
        public decimal Cost { get; } = 15m;
        public string ParcelType { get; } = "XL";
        
        public double WeightLimit { get; } = 10;
    }
}