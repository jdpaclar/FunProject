using System.Collections.Generic;
using ParcelApp.Common;

namespace ParcelApp.Business.Interface
{
    public interface IOrderBuilder
    {
        ParcelOrderOutput BuildOrder(ParcelOrder parcelOrder);
    }
}