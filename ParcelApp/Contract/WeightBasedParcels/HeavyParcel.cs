using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract.WeightBasedParcels
{
    public class HeavyParcel: IWeightParcel
    {
        public double Min { get; } = 0;
        public double Max { get; } = 50;
        public decimal OverCharge { get; } = 1m;
        public decimal InitialCost { get; } = 50m;
        
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.NotSupported;
    }
}