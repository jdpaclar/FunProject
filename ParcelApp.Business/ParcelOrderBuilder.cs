using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Interface;
using ParcelApp.Business;

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
            
            var parcelTypes = GetParcelTypeBySizes(parcelOrder.ParcelOrderItems);
            parcelOrderOutput.LineItems = parcelTypes.Select(p => new ParcelOrderOutputItem
            {
                ParcelType = p.ParcelType,
                Cost = p.Cost
            }).ToList();

            if (!parcelOrder.ParcelOrderItems.HaveValidParcelItems())
                throw new Exception("Invalid Process.");

            var parcelBySize = parcelOrder.ParcelOrderItems.Where(p => p.CalculateBySize);
            var parcelByWeight = parcelOrder.ParcelOrderItems.Where(p => !p.CalculateBySize);

            ICostCalculator calculator;

            if (parcelBySize.Any())
            {
                calculator = new SizeBasedCostCalculator(_parcelClassifier);
                parcelOrderOutput.TotalCost = calculator.GetTotalCost(parcelBySize);
            }

            if (parcelByWeight.Any())
            {
                calculator = new WeightBasedCostCalculator(_parcelClassifier);
                parcelOrderOutput.TotalCost = calculator.GetTotalCost(parcelBySize);
            }

            if (parcelOrder.Speedy)
                parcelOrderOutput.TotalCost *= 2;

            return parcelOrderOutput;
        }

        private decimal CalculateWeightBasedCost(ParcelOrderItem parcelOrderItem)
        {
            var heavyType = _parcelClassifier.ClassifyHeavyParcelByWeight(parcelOrderItem.Weight);
            var weightLimit = heavyType.Max;

            if (parcelOrderItem.Weight <= weightLimit)
                return heavyType.InitialCost;
            
            var weightDifference = parcelOrderItem.Weight - weightLimit;
            return heavyType.InitialCost + (decimal)weightDifference;
        }
        
        private IEnumerable<ISizeParcel> GetParcelTypeBySizes(IEnumerable<ParcelOrderItem> sizes) =>
            sizes.Select(itm => _parcelClassifier.ClassifyParcelBySize(itm.Size)).ToList();
    }
}