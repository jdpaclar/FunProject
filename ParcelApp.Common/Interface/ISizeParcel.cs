using ParcelApp.Common.Constants;

namespace ParcelApp.Common.Interface
{
     public interface ISizeParcel: IParcelDiscount
     {
         public double Min { get; }
         public double Max { get; }
         public decimal Cost { get; }
         public string ParcelType { get; }
         public double WeightLimit { get; }
     }
}