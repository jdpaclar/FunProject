using System;
using ParcelApp.Common;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Test.Helpers
{
    public class MockSomethingSmallSizeParcel : ISizeParcel
    {
        public double Min { get; } = 5;
        public double Max { get; } = 6;
        public decimal Cost { get; } = 11m;
        public string ParcelType { get; } = "Small";
        public double WeightLimit { get; } = 1;
    }
    
    public class MockSomethingLargeSizeParcel : ISizeParcel
    {
        public double Min { get; } = 7;
        public double Max { get; } = 8;
        public decimal Cost { get; } = 11.5m;
        public string ParcelType { get; } = "Medium";
        public double WeightLimit { get; }
    }

    public class MockWeightBasedParcel : IWeightParcel
    {
        public double Min { get; } = 0;
        public double Max { get; } = 50;
        public decimal OverCharge { get; } = 1m;
        public decimal InitialCost { get; } = 50m;
    }
}