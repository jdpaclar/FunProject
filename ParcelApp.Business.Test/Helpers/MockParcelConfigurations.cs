using System;
using ParcelApp.Common;

namespace ParcelApp.Business.Test.Helpers
{
    public class MockSomethingSmallParcel : IParcel
    {
        public double Min { get; } = 5;
        public double Max { get; } = 6;
        public decimal Cost { get; } = 11m;
        public string ParcelType { get; } = "Small";
    }
    
    public class MockSomethingLargeParcel : IParcel
    {
        public double Min { get; } = 7;
        public double Max { get; } = 8;
        public decimal Cost { get; } = 11.5m;
        public string ParcelType { get; } = "Medium";
    }

    public class MockRidiculouslyLargeParcel : IParcel
    {
        public double Min { get; } = double.MaxValue;
        public double Max { get; } = double.MaxValue + 1;
        public decimal Cost { get; } = decimal.MaxValue;
        public string ParcelType { get; } = "BOOM!";
    }
}