using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Common.Discount
{
    public class SmallDiscount : IDiscount
    {
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.Small;
        public int Limit { get; } = 4;
    }
}