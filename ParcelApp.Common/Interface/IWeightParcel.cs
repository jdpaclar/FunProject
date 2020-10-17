using System.Security.Cryptography;

namespace ParcelApp.Common.Interface
{
    public interface IWeightParcel
    {
        public double Min { get; }
        public double Max { get; }
        public decimal OverCharge { get; }
        public decimal InitialCost { get; }
    }
}