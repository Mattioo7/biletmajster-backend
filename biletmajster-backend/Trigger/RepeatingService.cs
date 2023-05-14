using AutoMapper;
using biletmajster_backend.Database.Interfaces;
using System.Data;

namespace biletmajster_backend.Trigger
{
    public class RepeatingService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new (TimeSpan.FromSeconds(1));
        private readonly ILogger<RepeatingService> _logger;

        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public RepeatingService(IServiceProvider services, ILogger<RepeatingService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceProvider = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(await _timer.WaitForNextTickAsync(stoppingToken)&& !stoppingToken.IsCancellationRequested)
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    var scopedService = scope.ServiceProvider.GetRequiredService<IModelEventRepository>();
                    await scopedService.UpdateEventStatus();
                    await UpdateEventStatus();
                }
            }
        }
        private static async Task UpdateEventStatus()
        {
            Console.WriteLine("www");
        }
    }
}
