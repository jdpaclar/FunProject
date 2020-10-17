using System.Collections;
using System.Collections.Generic;
using ParcelApp.Common;
using ParcelApp.Common.Interface;
using System.Linq;
using ParcelApp.Business.Interface;
using ParcelApp.Common.Constants;

namespace ParcelApp.Business
{
    // public class DiscountCalculator
    // {
    //     private readonly IEnumerable<IDiscount> _configuredDiscounts;
    //     private readonly IParcelClassifier _parcelClassifier;
    //
    //     public DiscountCalculator(IEnumerable<IDiscount> configuredDiscounts, IParcelClassifier parcelClassifier)
    //     {
    //         _configuredDiscounts = configuredDiscounts;
    //         _parcelClassifier = parcelClassifier;
    //     }
    //
    //     public AppliedDiscount CalculateDiscount(IEnumerable<ParcelOrderItem> parcelOrderItems)
    //     {
    //         // apply simple discount
    //         var simpleDiscount = _configuredDiscounts.Where(d => !d.DiscountTypes.Equals(DiscountTypes.Mixed));
    //         
    //         var parcelMatchedGrouped = parcelOrderItems.Select(items =>
    //         {
    //             var parcelType = "";
    //             DiscountTypes discountType; 
    //
    //             if (items.CalculationType.Equals(CalculationType.BySize))
    //             {
    //                 var config = _parcelClassifier.ClassifyParcelBySize(items.Size);
    //                 parcelType = config.ParcelType;
    //                 discountType = config.DiscountTypes;
    //             }
    //             else
    //             {
    //                 var config = _parcelClassifier.ClassifyHeavyParcelByWeight(items.Weight);
    //                 discountType = config.DiscountTypes;
    //             }
    //
    //             return new
    //             {
    //                 Size = items.Size,
    //                 Weight = items.Weight,
    //                 ParcelType = parcelType,
    //                 DiscountTypes = discountType
    //             };
    //         }).GroupBy(grp => new { grp.ParcelType, grp.DiscountTypes});
    //
    //         parcelMatchedGrouped.ToList().ForEach(grp =>
    //         {
    //             var discountType = grp.Select(g => g.DiscountTypes).Single();
    //             // if not single somethings wrong
    //
    //             var discountConfig = simpleDiscount.Single(s => s.DiscountTypes.Equals(discountType));
    //             var limit = discountConfig.Limit;
    //             
    //             
    //
    //         });
    //
    //         // validate multiple discounts configured
    //         simpleDiscount.ToList().ForEach(discountConfig =>
    //         {
    //             var discountType = discountConfig.DiscountTypes;
    //
    //            
    //                 
    //         });
    //         
    //             
    //         // apply mixed logic
    //     }
    // }
}