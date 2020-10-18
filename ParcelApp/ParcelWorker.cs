using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParcelApp.Business.Interface;
using ParcelApp.Common;
using ParcelApp.Contract;
using ParcelApp.Interface;

namespace ParcelApp
{
    public class ParcelWorker: IParcelWorker
    {
        private readonly IOrderBuilder _parcelOrderBuilder;
        private readonly ILogger<ParcelWorker> _logger;

        public ParcelWorker(IOrderBuilder parcelOrderBuilder, ILogger<ParcelWorker> logger)
        {
            _parcelOrderBuilder = parcelOrderBuilder;
            _logger = logger;
        }

        public void ExecuteOrder(ParcelOrder order)
        {
            try
            {
                _logger.LogInformation("Processing Order.");
                
                var output = _parcelOrderBuilder.BuildOrder(order);
                PrintOutput(output);
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to Process Order.", e);
            }
        }
        
        private static void PrintOutput(ParcelOrderOutput parcelOrder)
        {
            var display = Mapper.Map<ParcelOutputDisplay>(parcelOrder);
            
            var generatedOrder = JsonConvert.SerializeObject(display);
            Console.WriteLine(generatedOrder);
        }
    }
}