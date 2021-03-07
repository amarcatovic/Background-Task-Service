using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackgroundTaskService.API.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BackgroundTaskService.API.Services
{
    public class NumberHostedService : BackgroundService
    {
        private readonly ILogger<NumberHostedService> _logger;
        private readonly INumberService _numberService;
        private readonly NumberIncrementConfiguration _configOptions;

        public NumberHostedService(ILogger<NumberHostedService> logger, 
            INumberService numberService,
            IOptions<NumberIncrementConfiguration> configOptions)
        {
            _logger = logger;
            _numberService = numberService;
            _configOptions = configOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _numberService.Number++;
                _logger.LogInformation($"Number: {_numberService.Number}");

                await Task.Delay(new TimeSpan(_configOptions.Hours, _configOptions.Minutes, _configOptions.Seconds));
            }
        }
    }
}
