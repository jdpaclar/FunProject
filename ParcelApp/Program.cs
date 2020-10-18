using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParcelApp.Business;
using ParcelApp.Business.Calculators;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Constants;
using ParcelApp.Common.Discount;
using ParcelApp.Common.Interface;
using ParcelApp.Contract;
using ParcelApp.Contract.WeightBasedParcels;
using AutoMapper;
using ParcelApp.Interface;

namespace ParcelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var worker = serviceProvider.GetService<IParcelWorker>();

            var order = new ParcelOrder
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
                    new ParcelOrderItem(9, 5, CalculationType.BySize),
                    new ParcelOrderItem(9, 1, CalculationType.BySize),
                    new ParcelOrderItem(9, 1, CalculationType.BySize),
                    new ParcelOrderItem(49, 50, CalculationType.BySize),
                    new ParcelOrderItem(49, 4, CalculationType.BySize),
                    new ParcelOrderItem(51, 100, CalculationType.BySize),
                    new ParcelOrderItem(101, 1000, CalculationType.BySize),
                    new ParcelOrderItem(101, 50, CalculationType.ByWeight),
                    new ParcelOrderItem(101, 51, CalculationType.ByWeight),
                }
            };

            worker.ExecuteOrder(order);

            serviceProvider.Dispose();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            Mapper.Initialize(InitializeMappers);
            
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
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<IParcelClassifier, ParcelClassifier>()
                .AddSingleton<IDiscountCalculator, DiscountCalculator>()
                .AddSingleton<IOrderBuilder, ParcelOrderBuilder>()
                .AddSingleton<IParcelWorker, ParcelWorker>();
        }
        
        private static void InitializeMappers(IMapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<ParcelOrderOutput, ParcelOutputDisplay>()
                .ForMember(dest => dest.Speedy, opts => opts.MapFrom(src => src.IsSpeedy ? "Yes" : "No"))
                .ForMember(dest => dest.SavedCost, opts => opts.MapFrom(src => $"-${src.TotalSaved}"))
                .ForMember(dest => dest.TotalCost, opts => opts.MapFrom(src => $"${src.TotalCost}"));

            mapperConfiguration.CreateMap<ParcelOrderOutputItem, ParcelOutputLineDisplay>();
        }
    }
}