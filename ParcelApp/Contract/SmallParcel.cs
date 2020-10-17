using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Contract
{
    public class SmallParcel : IParcel
    {
        public double Min { get; } = 0;
        public double Max { get; } = 9;
        public decimal Cost { get; } = 3m;
        public string ParcelType { get; } = "Small";
        public double WeightLimit { get; } = 1;
    }

}