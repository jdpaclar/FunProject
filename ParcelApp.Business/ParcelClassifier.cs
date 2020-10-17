using System;
using System.Collections.Generic;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business
{
    public class ParcelClassifier : IParcelClassifier
    {
        private readonly List<ISizeParcel> _definedParcels;
        private readonly List<IWeightParcel> _definedHeavyParcels;

        public ParcelClassifier(List<ISizeParcel> definedParcels, List<IWeightParcel> definedHeavyParcels)
        {
            _definedParcels = definedParcels;
            _definedHeavyParcels = definedHeavyParcels;
        }

        public ISizeParcel ClassifyParcelBySize(double size)
        {
            var parcel = _definedParcels;
            
            if (!parcel.Any())
                throw new Exception("No Parcel Configured.");

            var identifiedParcel = parcel.SingleOrDefault(p => size >= p.Min && size <= p.Max);

            if (identifiedParcel == null)
                throw new NotSupportedException("Not Supported Parcel Configuration.");
            
            return identifiedParcel;
        }

        public IWeightParcel ClassifyHeavyParcelByWeight(double weight)
        {
            var heavyParcel = _definedHeavyParcels;

            if (!heavyParcel.Any())
                throw new Exception("No Heavy Parcel Configured.");
            
            return heavyParcel.Single();
        }
    }

}