using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;

namespace ParcelApp.Business
{
    public class WeightBasedCostCalculator: ICostCalculator
    {
        private readonly IParcelClassifier _parcelClassifier;

        public WeightBasedCostCalculator(IParcelClassifier parcelClassifier)
        {
            _parcelClassifier = parcelClassifier;
        }

        public decimal GetTotalCost(IEnumerable<ParcelOrderItem> parcelOrderItems) =>
            parcelOrderItems.Select(GetCostPerLine).Sum();

        private decimal GetCostPerLine(ParcelOrderItem parcelOrderItem)
        {
            var heavyType = _parcelClassifier.ClassifyHeavyParcelByWeight(parcelOrderItem.Weight);
            var weightLimit = heavyType.Max;

            if (parcelOrderItem.Weight <= weightLimit)
                return heavyType.InitialCost;
            
            var weightDifference = parcelOrderItem.Weight - weightLimit;
            return heavyType.InitialCost + ((decimal)weightDifference * heavyType.OverCharge);
        }
    }
}