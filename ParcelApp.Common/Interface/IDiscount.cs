using ParcelApp.Common.Constants;

namespace ParcelApp.Common.Interface
{
    public interface IDiscount
    {
        public DiscountTypes DiscountTypes { get; }
        public int Limit { get; }
    }
}