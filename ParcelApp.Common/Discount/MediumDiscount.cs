using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Common.Discount
{
    public class MediumDiscount: IDiscount
    {
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.Medium;
        public int Limit { get; } = 3;
    }
}