using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;

namespace ParcelApp.Business
{
    public class SizeBasedCostCalculator: ICostCalculator
    {
        private readonly IParcelClassifier _parcelClassifier;

        public SizeBasedCostCalculator(IParcelClassifier parcelClassifier)
        {
            _parcelClassifier = parcelClassifier;
        }
        
        public decimal GetTotalCost(IEnumerable<ParcelOrderItem> parcelOrderItems)
        {
            var totalCost = 0m;
            
            totalCost = CalculateTotalCost(parcelOrderItems);
            totalCost += CalculateWeightAddOn(parcelOrderItems);

            return totalCost;
        }
        
        private decimal CalculateTotalCost(IEnumerable<ParcelOrderItem> parcelOrderItems)
        {
            return parcelOrderItems.Select(po => _parcelClassifier.ClassifyParcelBySize(po.Size).Cost).Sum();
        }
        
        private decimal CalculateWeightAddOn(IEnumerable<ParcelOrderItem> parcelOrderItems)
        {
            return parcelOrderItems.Select(oi =>
            {
                var addOn = 0m;
                var parcelType = _parcelClassifier.ClassifyParcelBySize(oi.Size);
                var weightLimit = parcelType.WeightLimit;
        
                if (oi.Weight > weightLimit)
                    addOn += 2m;
        
                return addOn;
            }).Sum();
        }
    }
}