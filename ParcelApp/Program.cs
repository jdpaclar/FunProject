using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParcelApp.Business;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Constants;
using ParcelApp.Common.Discount;
using ParcelApp.Common.Interface;
using ParcelApp.Contract;
using ParcelApp.Contract.WeightBasedParcels;

namespace ParcelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var worker = serviceProvider.GetService<IOrderBuilder>();
            
            var order = worker?.BuildOrder(new ParcelOrder
            {
                Speedy = true,
                DiscountToApply = new List<DiscountTypes>
                {
                    DiscountTypes.Small,
                    DiscountTypes.Medium
                },
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(9, 1, CalculationType.BySize),
                    new ParcelOrderItem(49, 50, CalculationType.BySize),
                    new ParcelOrderItem(49, 4, CalculationType.BySize),
                    new ParcelOrderItem(51, 100, CalculationType.BySize),
                    new ParcelOrderItem(101, 1000, CalculationType.BySize),
                    new ParcelOrderItem(101, 50, CalculationType.ByWeight),
                    new ParcelOrderItem(101, 51, CalculationType.ByWeight),
                }
            });
            
            PrintOutput(order);

            serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configure Parcel Types
            var sizeBasedParcels = new List<ISizeParcel>
            {
                new SmallSizeParcel(),
                new MediumSizeParcel(),
                new LargeSizeParcel(),
                new ExtraLargeSizeParcel()
            };

            var weightBasedParcels = new List<IWeightParcel>
            {
                new HeavyParcel()
            };

            services.AddSingleton(sizeBasedParcels);
            services.AddSingleton(weightBasedParcels);
            
            // Configure Enabled Discounts
            var discountConfig = new List<IDiscount>
            {
                new SmallDiscount(),
                new MediumDiscount(),
                new MixedDiscount()
            };

            services.AddSingleton(discountConfig);

            services
                .AddSingleton<IParcelClassifier, ParcelClassifier>()
                .AddSingleton<IOrderBuilder, ParcelOrderBuilder>()
                .AddSingleton<IDiscountCalculator, DiscountCalculator>();
        }

        private static void PrintOutput(ParcelOrderOutput parcelOrder)
        {
            var generatedOrder = JsonConvert.SerializeObject(parcelOrder);
            Console.WriteLine(generatedOrder);
        }
    }
}