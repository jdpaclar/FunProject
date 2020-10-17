using ParcelApp.Common;
using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class SmallSizeParcel : ISizeParcel
    {
        public double Min { get; } = 0;
        public double Max { get; } = 9;
        public decimal Cost { get; } = 3m;
        public string ParcelType { get; } = ParcelTypes.Small.ToString();
        public double WeightLimit { get; } = 1;
        
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.Small;
    }

}