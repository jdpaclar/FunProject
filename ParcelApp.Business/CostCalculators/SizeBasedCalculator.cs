using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.CostCalculators
{
    public static class SizeBasedCalculator
    {
        public static decimal GetCostPerLine(double weight, ISizeParcel parcelConfiguration)
        {
            var initialCost = parcelConfiguration.Cost;
            var weightLimit = parcelConfiguration.WeightLimit;

            if (!(weight > weightLimit)) return initialCost;
            
            var difference = weight - weightLimit;
            return initialCost + (decimal)(difference * 2);;
        }
    }
}