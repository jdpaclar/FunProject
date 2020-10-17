using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business
{
    public class ParcelClassifier : IParcelClassifier
    {
        private readonly List<IParcel> _definedParcels;
        
        public ParcelClassifier(List<IParcel> definedParcels)
        {
            _definedParcels = definedParcels;
        }

        public IParcel ClassifyParcelBySize(double size)
        {
            var parcel = _definedParcels;
            
            if (!parcel.Any())
                throw new Exception("No Parcel Configured.");

            var identifiedParcel = parcel.SingleOrDefault(p => size >= p.Min && size <= p.Max);

            if (identifiedParcel == null)
                throw new NotSupportedException("Not Supported Parcel Configuration.");
            
            return identifiedParcel;
        }
    }

}