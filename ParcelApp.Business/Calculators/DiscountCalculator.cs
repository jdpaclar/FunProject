using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Calculators
{
    public class DiscountCalculator: IDiscountCalculator
    {
        private readonly List<IDiscount> _configuredDiscounts;

        public DiscountCalculator(List<IDiscount> configuredDiscounts)
        {
            _configuredDiscounts = configuredDiscounts;
        }

        public AppliedDiscount CalculateMixedDiscount(IEnumerable<ParcelOrderOutputItem> items)
        {
            var appliedDiscount = new AppliedDiscount
            {
                TotalCost = items.Select(i => i.Cost).Sum()
            };
            
            var discountRule = (_configuredDiscounts.Where(t => t.DiscountTypes.Equals(DiscountTypes.Mixed))).SingleOrDefault();

            if (discountRule == null)
                throw new NotSupportedException("Discount Ruleset not supported.");
            
            var totalCounts = items.Count();

            var groups = Math.Abs(totalCounts / discountRule.Limit);
            if (groups <= 0) return appliedDiscount;
            var i = 0;

            var modifiedGroup = items;
                    
            do
            {
                var min = modifiedGroup.Select(m => m.Cost).Min();
                appliedDiscount.SavedCost += min;
                appliedDiscount.TotalCost -= min;
                modifiedGroup = modifiedGroup.Where(c => !c.Cost.Equals(min)).ToList();
                i += 1;
            } while (i < groups);

            return appliedDiscount;
        }

        public AppliedDiscount CalculateDiscount(IEnumerable<ParcelOrderOutputItem> items)
        {
            var appliedDiscount = new AppliedDiscount
            {
                TotalCost = items.Select(i => i.Cost).Sum()
            };

            var groupedByParcelDiscountType = items.GroupBy(g => new
            {
                g.DiscountTypes,
                g.ParcelType
            });
            
            groupedByParcelDiscountType.ToList().ForEach(grp =>
            {
                var discountRule = (_configuredDiscounts.Where(t => t.DiscountTypes.Equals(grp.Key.DiscountTypes))).SingleOrDefault();

                if (discountRule != null)
                {
                    var totalCounts = grp.Count();
                    var groups = Math.Abs(totalCounts / discountRule.Limit);

                    if (groups <= 0) return;
                    var i = 0;

                    IEnumerable<ParcelOrderOutputItem> modifiedGroup = grp;
                    
                    do
                    {
                        var min = modifiedGroup.Select(m => m.Cost).Min();
                        appliedDiscount.SavedCost += min;
                        appliedDiscount.TotalCost -= min;
                        modifiedGroup = grp.Where(c => !c.Cost.Equals(min)).ToList();
                        i += 1;
                    } while (i < groups);
                }
            });
            
            return appliedDiscount;
        }
    }
}