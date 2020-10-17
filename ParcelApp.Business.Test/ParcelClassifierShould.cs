using System;
using System.Collections.Generic;
using ParcelApp.Business.Interface;
using ParcelApp.Business.Test.Helpers;
using Xunit;
using FluentAssertions;
using ParcelApp.Common.Interface;

namespace ParcelApp.Business.Test
{
    public class ParcelClassifierShould
    {
        private readonly IParcelClassifier _parcelClassifier;
        
        public ParcelClassifierShould()
        {
            var definedParcelTypes = new List<ISizeParcel>
            {
                new MockSomethingSmallSizeParcel(),
                new MockSomethingLargeSizeParcel()
            };
            
            _parcelClassifier = new ParcelClassifier(definedParcelTypes, new List<IWeightParcel>());
        }

        [Fact]
        public void FailLoudly_When_NoParcelRule_IsDefined()
        {
            // arrange
            var parcelClassifier = new ParcelClassifier(new List<ISizeParcel>(), new List<IWeightParcel>());
            
            // Act
            var exception = Assert.Throws<Exception>(() => parcelClassifier.ClassifyParcelBySize(double.MaxValue));

            // Assert
            exception.Message.Should().Be("No Parcel Configured.");
        }
        
        [Fact]
        public void FailLoudly_When_ParcelRule_IsNot_InConfiguredList()
        {
            var exception = Assert.Throws<NotSupportedException>(() => _parcelClassifier.ClassifyParcelBySize(double.MaxValue));

            // Assert
            exception.Message.Should().Be("Not Supported Parcel Configuration.");
        }
        
        [Fact]
        public void ProperlyDetermine_CorrectCost_By_ParcelSize()
        {
            // arrange
            var smallParcel = new MockSomethingSmallSizeParcel();
            
            // act
            var parcel = _parcelClassifier.ClassifyParcelBySize(5);

            // assert
            parcel.Should().BeEquivalentTo(smallParcel);
        }
    }
}