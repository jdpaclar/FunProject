using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Common.Discount
{
    public class MixedDiscount: IDiscount
    {
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.Mixed;
        public int Limit { get; } = 5;
    }
}