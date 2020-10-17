using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using Moq;
using ParcelApp.Business.Interface;
using ParcelApp.Business.Test.Helpers;
using ParcelApp.Common;
using ParcelApp.Common.Constants;

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
                    new ParcelOrderItem(5, 1, CalculationType.BySize)
                },
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);


            // assert
            order.LineItems.Count.Should().Be(1);
            order.LineItems.Single().Cost.Should().Be(smallParcelType.Cost);
            order.LineItems.Single().ParcelType.Should().Be(smallParcelType.ParcelType.ToString());
        }
        
        [Theory]
        [InlineData(5, 1, true, CalculationType.BySize, 22)]
        [InlineData(5, 1,false, CalculationType.BySize, 11)]
        [InlineData(5, 3,false, CalculationType.BySize, 15)]
        [InlineData(5, 2, true, CalculationType.ByWeight, 100)]
        [InlineData(5, 50, true, CalculationType.ByWeight, 100)]
        [InlineData(5, 52, true, CalculationType.ByWeight, 104)]
        [InlineData(5, 50, false, CalculationType.ByWeight, 50)]
        public void ProperlyComputeCostBySize(double size, double weight, bool isSpeedy, CalculationType calculationType, decimal expectedTotalCost)
        {
            // arrange
            var smallParcelType = new MockSomethingSmallSizeParcel();
            var weightBasedParcel = new MockWeightBasedParcel();
            
            _mockParcelClassifier.Setup(p => p.ClassifyParcelBySize(It.IsAny<double>()))
                .Returns(smallParcelType);

            _mockParcelClassifier.Setup(p => p.ClassifyHeavyParcelByWeight(It.IsAny<double>()))
                .Returns(weightBasedParcel);

            var parcelOrder = new ParcelOrder
            {
                Speedy = isSpeedy,
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(size, weight,  calculationType)
                }
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);

            // assert
            order.TotalCost.Should().Be(expectedTotalCost);
        }

        [Theory]
        [InlineData(true, 250)]
        [InlineData(false, 125)]
        public void ProperlyCompute_CompoundOrders(bool isSpeedy, decimal expectedTotal)
        {
            // arrange
            var smallParcelType = new MockSomethingSmallSizeParcel();
            var weightBasedParcel = new MockWeightBasedParcel();
            
            _mockParcelClassifier.Setup(p => p.ClassifyParcelBySize(It.IsAny<double>()))
                .Returns(smallParcelType);

            _mockParcelClassifier.Setup(p => p.ClassifyHeavyParcelByWeight(It.IsAny<double>()))
                .Returns(weightBasedParcel);

            var parcelOrder = new ParcelOrder
            {
                Speedy = isSpeedy,
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(1, 2,  CalculationType.BySize),
                    new ParcelOrderItem(1, 1,  CalculationType.BySize),
                    new ParcelOrderItem(1, 50,  CalculationType.ByWeight),
                    new ParcelOrderItem(1, 51,  CalculationType.ByWeight)
                }
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);

            // assert
            order.TotalCost.Should().Be(expectedTotal);
        }

        // [Fact]
        // public void ProperlyCompute_Discounts()
        // {
        //     // arrange
        //     var mediumParcel = new MockMediumParcel();
        //     var weightBasedParcel = new MockWeightBasedParcel();
        //     
        //     _mockParcelClassifier.Setup(p => p.ClassifyParcelBySize(It.IsAny<double>()))
        //         .Returns(mediumParcel);
        //
        //     _mockParcelClassifier.Setup(p => p.ClassifyHeavyParcelByWeight(It.IsAny<double>()))
        //         .Returns(weightBasedParcel);
        //
        //     var parcelOrder = new ParcelOrder
        //     {
        //         Speedy = isSpeedy,
        //         ParcelOrderItems = new List<ParcelOrderItem>
        //         {
        //             new ParcelOrderItem(1, 2,  CalculationType.BySize),
        //             new ParcelOrderItem(1, 1,  CalculationType.BySize),
        //             new ParcelOrderItem(1, 50,  CalculationType.ByWeight),
        //             new ParcelOrderItem(1, 51,  CalculationType.ByWeight)
        //         }
        //     };
        //     
        //     // act
        //     var order = _parcelBuilder.BuildOrder(parcelOrder);
        //
        //     // assert
        //     order.TotalCost.Should().Be(expectedTotal);
        // }

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
                    new ParcelOrderItem(5, 2, CalculationType.BySize),
                    new ParcelOrderItem(5, 1,  CalculationType.BySize)
                }
            };
            
            // act
            var order = _parcelBuilder.BuildOrder(parcelOrder);

            // assert
            order.TotalCost.Should().Be(24);
        }
    }
}