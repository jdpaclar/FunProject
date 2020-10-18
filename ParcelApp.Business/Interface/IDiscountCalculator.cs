using System.Collections.Generic;
using ParcelApp.Common;

namespace ParcelApp.Business.Interface
{
    public interface IDiscountCalculator
    {
        AppliedDiscount CalculateMixedDiscount(IEnumerable<ParcelOrderOutputItem> items);
        AppliedDiscount CalculateDiscount(IEnumerable<ParcelOrderOutputItem> items);
    }
}