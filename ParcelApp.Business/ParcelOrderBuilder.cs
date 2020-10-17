using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;

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
            
            parcelOrder.ParcelSizes.ForEach(size =>
            {
                var parcel = GenerateParcelOrderOutputItem(size);
                parcelOrderOutput.LineItems.Add(parcel);
            });

            parcelOrderOutput.TotalCost = CalculateTotalCost(parcelOrderOutput.LineItems, parcelOrder.Speedy);
            
            return parcelOrderOutput;
        }

        private decimal CalculateTotalCost(IReadOnlyList<ParcelOrderOutputItem> parcelOrderOutputItems, bool isSpeedy = false)
        {
            var totalCost = 0m;
            
            if (!parcelOrderOutputItems.Any())
                throw new ArgumentException("No Parcel Order Line Specified.");

            totalCost = parcelOrderOutputItems.Select(po => po.Cost).Sum();

            if (isSpeedy)
                totalCost *= 2;

            return totalCost;
        }

        private ParcelOrderOutputItem GenerateParcelOrderOutputItem(double parcelSize)
        {
            var parcelClassification = _parcelClassifier.ClassifyParcelBySize(parcelSize);
            
            return new ParcelOrderOutputItem
            {
                ParcelType = parcelClassification.ParcelType,
                Cost = parcelClassification.Cost
            };
        }
    }
}