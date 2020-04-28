using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace IHostedImplementation
{
    public class ServiceWorker : IHostedService
    {
        private readonly IHostApplicationLifetime _IHostApplicationLifetime;
        public ServiceWorker(IHostApplicationLifetime IHostApplicationLifetime)
        {

            _IHostApplicationLifetime = IHostApplicationLifetime;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _IHostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            _IHostApplicationLifetime.ApplicationStopped.Register(OnStopped);
            _IHostApplicationLifetime.ApplicationStarted.Register(OnStopped);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}