using ParcelApp.Common;

namespace ParcelApp.Contract
{
    public class MediumParcel : IParcel
    {
        public double Min { get; } = 10;
        public double Max { get; } = 49;
        public decimal Cost { get; } = 8m;
        
        public string ParcelType { get; } = "Medium";
    }
}