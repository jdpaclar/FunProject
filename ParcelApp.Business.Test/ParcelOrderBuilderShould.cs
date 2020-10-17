using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Moq;
using ParcelApp.Business.Interface;
using ParcelApp.Business.Test.Helpers;
using ParcelApp.Common;

namespace ParcelApp.Business.Test
{
    public class ParcelOrderBuilderShould
    {
        private readonly Mock<IParcelClassifier> _mockParcelClassifier;
        private readonly ParcelOrderBuilder _parcelBuilder;
        
        public ParcelOrderBuilderShould()
        {
            _mockParcelClassifier = new Mock<IParcelClassifier>();
            _parcelBuilder = new ParcelOrderBuilder(_mockParcelClassifier.Object);
        }

        [Fact]
        public void ProperlyGenerateOrderLines()
        {
            // arrange
            var smallParcelType = new MockSomethingSmallSizeParcel();
            
            _mockParcelClassifier.Setup(p => p.ClassifyParcelBySize(It.IsAny<double>()))
                .Returns(smallParcelType);
            
            var parcelOrder = new ParcelOrder
            {
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(5, 1)
                },
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);


            // assert
            order.LineItems.Count.Should().Be(1);
            order.LineItems.Single().Cost.Should().Be(smallParcelType.Cost);
            order.LineItems.Single().ParcelType.Should().Be(smallParcelType.ParcelType);
        }
        
        [Theory]
        [InlineData(true, 22)]
        [InlineData(false, 11)]
        public void ProperlyComputeCost(bool isSpeedy, decimal expectedTotalCost)
        {
            // arrange
            var smallParcelType = new MockSomethingSmallSizeParcel();
            
            _mockParcelClassifier.Setup(p => p.ClassifyParcelBySize(It.IsAny<double>()))
                .Returns(smallParcelType);
            
            var parcelOrder = new ParcelOrder
            {
                Speedy = isSpeedy,
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(5, 1, true)
                }
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);


            // assert
            order.TotalCost.Should().Be(expectedTotalCost);
        }

        [Fact]
        public void ProperlyComputeWeightAddOn()
        {
            // arrange
            var smallParcelType = new MockSomethingSmallSizeParcel();
            
            _mockParcelClassifier.Setup(p => p.ClassifyParcelBySize(It.IsAny<double>()))
                .Returns(smallParcelType);
            
            var parcelOrder = new ParcelOrder
            {
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(5, 2, true),
                    new ParcelOrderItem(5, 1, true)
                }
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);

            // assert
            order.TotalCost.Should().Be(24);
        }
    }
}