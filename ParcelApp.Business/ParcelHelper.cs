using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Common;

namespace ParcelApp.Business
{
    public static class ParcelHelper
    {
        public static bool HaveValidParcelItems(this IReadOnlyList<ParcelOrderItem> parcelOrder)
        {
            if (parcelOrder == null)
                throw new ArgumentException("Null Parcel Order Items.");
            
            if (!parcelOrder.Any())
                throw new ArgumentException("No Parcel Order Line Specified.");

            return true;
        }
    }
}