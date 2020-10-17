using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ParcelApp.Business;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
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

            var worker = serviceProvider.GetService<IParcelClassifier>();
            var tt = worker?.ClassifyParcelBySize(50);
            
            Console.WriteLine($"Parcel Type { tt?.ParcelType }");
            
            serviceProvider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configure Parcel Types
            var parcelTypes = new List<IParcel>
            {
                new SmallParcel(),
                new MediumParcel()
            };

            services.AddSingleton(parcelTypes);
            
            services
                .AddSingleton<IParcelClassifier, ParcelClassifier>();
        }
    }
}