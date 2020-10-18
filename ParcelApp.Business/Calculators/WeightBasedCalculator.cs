using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Calculators
{
    public static class WeightBasedCalculator
    {
        public static decimal GetCostPerLine(double weight, IWeightParcel parcelConfiguration)
        {
            var weightLimit = parcelConfiguration.Max;

            if (weight <= weightLimit)
                return parcelConfiguration.InitialCost;
            
            var weightDifference = weight - weightLimit;
            return parcelConfiguration.InitialCost + ((decimal)weightDifference * parcelConfiguration.OverCharge);
        }
    }
}