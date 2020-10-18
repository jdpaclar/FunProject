using ParcelApp.Common.Constants;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Test.Helpers
{
    public class MockDiscountConfiguration: IDiscount
    {
        public DiscountTypes DiscountTypes { get; } = DiscountTypes.Small;
        public int Limit { get; } = 2;
    }
}