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
            
            parcelOrderOutput.TotalCost = CalculateTotalCost(parcelOrder);
            parcelOrderOutput.TotalCost += CalculateWeightAddOn(parcelOrder.ParcelOrderItems);
            
            return parcelOrderOutput;
        }

        private decimal CalculateWeightAddOn(IReadOnlyList<ParcelOrderItem> parcelOrderItems)
        {
            if (!parcelOrderItems.HaveValidParcelItems())
                return 0m;
            
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

        private decimal CalculateTotalCost(ParcelOrder parcelOrder)
        {
            var totalCost = 0m;

            if (!parcelOrder.ParcelOrderItems.HaveValidParcelItems())
                return totalCost;
            
            totalCost = parcelOrder.ParcelOrderItems.Select(po => _parcelClassifier.ClassifyParcelBySize(po.Size).Cost).Sum();

            if (parcelOrder.Speedy)
                totalCost *= 2;

            return totalCost;
        }
        
        private IEnumerable<IParcel> GetParcelTypeBySizes(IEnumerable<ParcelOrderItem> sizes) =>
            sizes.Select(itm => _parcelClassifier.ClassifyParcelBySize(itm.Size)).ToList();
    }
}