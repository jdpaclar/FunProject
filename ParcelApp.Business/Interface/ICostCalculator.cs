using System.Collections.Generic;
using ParcelApp.Common;

namespace ParcelApp.Business.Interface
{
    public interface ICostCalculator
    {
        decimal GetTotalCost(IEnumerable<ParcelOrderItem> parcelOrder);
        
    }
}