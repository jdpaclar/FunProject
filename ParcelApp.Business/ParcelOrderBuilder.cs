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
        private readonly IDiscountCalculator _discountCalculator;

        public ParcelOrderBuilder(IParcelClassifier parcelClassifier, IDiscountCalculator discountCalculator)
        {
            _parcelClassifier = parcelClassifier;
            _discountCalculator = discountCalculator;
        }
        
        public ParcelOrderOutput BuildOrder(ParcelOrder parcelOrder)
        {
            if (!parcelOrder.ParcelOrderItems.HaveValidParcelItems())
                throw new Exception("Invalid Process.");
            
            var parcelOrderOutput = new ParcelOrderOutput();
            
            var parcelBySize = parcelOrder.ParcelOrderItems.Where(p => p.CalculationType.Equals(CalculationType.BySize));
            var parcelByWeight = parcelOrder.ParcelOrderItems.Where(p => p.CalculationType.Equals(CalculationType.ByWeight));
            
            
            if (parcelBySize.Any())
            {
                var lineOutputItems = parcelBySize.Select( s =>
                {
                    var config = _parcelClassifier.ClassifyParcelBySize(s.Size);
                    var cost = SizeBasedCalculator.GetCostPerLine(s.Weight, config);

                    return new ParcelOrderOutputItem
                    {
                        ParcelType = config.ParcelType,
                        Cost = cost,
                        DiscountTypes = config.DiscountTypes
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
                        Cost = cost,
                        DiscountTypes = config.DiscountTypes
                    };
                });
                
                parcelOrderOutput.LineItems.AddRange(lineOutputItems);
                parcelOrderOutput.TotalCost += lineOutputItems.Select(i => i.Cost).Sum();
            }

            // apply discounts
            if (parcelOrder.DiscountToApply.Any())
            {
                AppliedDiscount discount;
                
                if (parcelOrder.DiscountToApply.Contains(DiscountTypes.Mixed))
                {
                    discount = _discountCalculator.CalculateMixedDiscount(parcelOrderOutput.LineItems);
                }
                else
                {
                    discount = _discountCalculator.CalculateDiscount(parcelOrderOutput.LineItems);
                }

                parcelOrderOutput.TotalCost = discount.TotalCost;
                parcelOrderOutput.TotalSaved = discount.SavedCost;
            }
            
            if (parcelOrder.Speedy)
                parcelOrderOutput.TotalCost *= 2;

            return parcelOrderOutput;
        }
    }
}