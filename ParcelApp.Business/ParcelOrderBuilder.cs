using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Interface;
using ParcelApp.Business;
using ParcelApp.Business.CostCalculators;
using ParcelApp.Common.Constants;

namespace ParcelApp.Business
{
    public class ParcelOrderBuilder : IOrderBuilder
    {
        private readonly IParcelClassifier _parcelClassifier;

        public ParcelOrderBuilder(IParcelClassifier parcelClassifier)
        {
            _parcelClassifier = parcelClassifier;
        }
        
        public ParcelOrderOutput BuildOrder(ParcelOrder parcelOrder)
        {
            var parcelOrderOutput = new ParcelOrderOutput();
            
            var parcelBySize = parcelOrder.ParcelOrderItems.Where(p => p.CalculationType.Equals(CalculationType.BySize));
            var parcelByWeight = parcelOrder.ParcelOrderItems.Where(p => p.CalculationType.Equals(CalculationType.ByWeight));
            
            if (!parcelOrder.ParcelOrderItems.HaveValidParcelItems())
                throw new Exception("Invalid Process.");
            
            if (parcelBySize.Any())
            {
                var lineOutputItems = parcelBySize.Select(s =>
                {
                    var config = _parcelClassifier.ClassifyParcelBySize(s.Size);
                    var cost = SizeBasedCalculator.GetCostPerLine(s.Weight, config);

                    return new ParcelOrderOutputItem
                    {
                        ParcelType = config.ParcelType,
                        Cost = cost
                    };
                });
                
                parcelOrderOutput.LineItems.AddRange(lineOutputItems);
                parcelOrderOutput.TotalCost += lineOutputItems.Select(i => i.Cost).Sum();
            }
            
            if (parcelByWeight.Any())
            {
                var lineOutputItems = parcelByWeight.Select(s =>
                {
                    var config = _parcelClassifier.ClassifyHeavyParcelByWeight(s.Weight);
                    var cost = WeightBasedCalculator.GetCostPerLine(s.Weight, config);

                    return new ParcelOrderOutputItem
                    {
                        ParcelType = "HeavyParcel",
                        Cost = cost
                    };
                });
                
                parcelOrderOutput.LineItems.AddRange(lineOutputItems);
                parcelOrderOutput.TotalCost += lineOutputItems.Select(i => i.Cost).Sum();
            }

            if (parcelOrder.Speedy)
                parcelOrderOutput.TotalCost *= 2;

            return parcelOrderOutput;
        }
    }
}