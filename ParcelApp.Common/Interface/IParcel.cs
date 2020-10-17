namespace ParcelApp.Common.Interface
{
     public interface IParcel
     {
         public double Min { get; }
         public double Max { get; }
         public decimal Cost { get; }
         public string ParcelType { get; }
         public double WeightLimit { get; }
     }
}