using System.Collections.Generic;
using FluentAssertions;
using ParcelApp.Common;
using ParcelApp.Common.Constants;
using ParcelApp.Common.Discount;
using ParcelApp.Common.Interface;
using Xunit;

namespace ParcelApp.Business.Test
{
    public class DiscountCalculatorShould
    {
        private readonly DiscountCalculator _discountCalculator;
        
        public DiscountCalculatorShould()
        {
            _discountCalculator = new DiscountCalculator(new List<IDiscount>
            {
                new SmallDiscount(),
                new MediumDiscount(),
                new MixedDiscount()
            });
        }

        [Fact]
        public void ProperlyComputeMixedDiscount()
        {
            // arrange 
            var outputItems = new List<ParcelOrderOutputItem>
            {
                new ParcelOrderOutputItem
                {
                    Cost = 8m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 2m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 10m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 10m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },

                new ParcelOrderOutputItem
                {
                    Cost = 8m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 2m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                }
            };
            
            // act
            var appliedDiscount = _discountCalculator.CalculateMixedDiscount(outputItems);
            
            // assert
            appliedDiscount.SavedCost.Should().Be(2m);
            appliedDiscount.TotalCost.Should().Be(38m);
        }
        
        [Fact]
        public void ProperlyComputeCompoundDiscount()
        {
            // arrange
            var outputItems = new List<ParcelOrderOutputItem>
            {
                new ParcelOrderOutputItem
                {
                    Cost = 8m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 2m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 10m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 10m,
                    DiscountTypes = DiscountTypes.Medium,
                    ParcelType = "Medium"
                },
                
                new ParcelOrderOutputItem
                {
                    Cost = 8m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 2m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 10m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 20m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                
                new ParcelOrderOutputItem
                {
                    Cost = 21m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 22m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 23m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                },
                new ParcelOrderOutputItem
                {
                    Cost = 24m,
                    DiscountTypes = DiscountTypes.Small,
                    ParcelType = "Small"
                }
            };

            // act
            var appliedDiscount = _discountCalculator.CalculateDiscount(outputItems);

            // assert
            appliedDiscount.SavedCost.Should().Be(12m);
            appliedDiscount.TotalCost.Should().Be(148m);
        }
    }
}