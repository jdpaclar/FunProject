using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class ExtraLargeSizeParcel: ISizeParcel
    {
        public double Min { get; } = 100;
        public double Max { get; } = double.MaxValue;
        public decimal Cost { get; } = 25m;
        public string ParcelType { get; } = ParcelTypes.ExtraLarge.ToString();
        public double WeightLimit { get; } = 10;

        public DiscountTypes DiscountTypes { get; } = DiscountTypes.NotSupported;
    }
}