using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParcelApp.Business;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Common.Interface;
using ParcelApp.Contract;

namespace ParcelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var worker = serviceProvider.GetService<IOrderBuilder>();
            
            var order = worker?.BuildOrder(new ParcelOrder
            {
                Speedy = true,
                ParcelOrderItems = new List<ParcelOrderItem>
                {
                    new ParcelOrderItem(9, 1, CalculationType.BySize),
                    new ParcelOrderItem(49, 50, CalculationType.BySize),
                }
            });
            
            PrintOutput(order);

            serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configure Parcel Types
            var parcelTypes = new List<ISizeParcel>
            {
                new SmallSizeParcel(),
                new MediumSizeParcel()
            };

            services.AddSingleton(parcelTypes);
            
            services
                .AddSingleton<IParcelClassifier, ParcelClassifier>()
                .AddSingleton<IOrderBuilder, ParcelOrderBuilder>();
        }

        private static void PrintOutput(ParcelOrderOutput parcelOrder)
        {
            var generatedOrder = JsonConvert.SerializeObject(parcelOrder);
            Console.WriteLine(generatedOrder);
        }
    }
}