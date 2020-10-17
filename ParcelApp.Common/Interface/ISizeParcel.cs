namespace ParcelApp.Common.Interface
{
     public interface ISizeParcel
     {
         public double Min { get; }
         public double Max { get; }
         public decimal Cost { get; }
         public ParcelTypes ParcelType { get; }
         public double WeightLimit { get; }
     }

     public enum ParcelTypes
     {
         Small,
         Medium,
         Large,
         ExtraLarge
     }
}