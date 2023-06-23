using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System;
using ApiApplication.ViewModels.Shared;
using ApiApplication.ViewModels.Models;
using System.Net.Http;

namespace ApiApplication.Services
{
    public class ImdbStatusBackgroundService : IHostedService, IDisposable
    {
        private Timer timer;
        private readonly ImdbStatusModel imdbStatus;

        public ImdbStatusBackgroundService(ImdbStatusModel imdbStatus)
        {
            this.imdbStatus = imdbStatus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(UpdateIMDBStatus, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void UpdateIMDBStatus(object state)
        {
            imdbStatus.Up = await FetchIMDBStatus(); 
            imdbStatus.LastCall= DateTime.UtcNow;
        }

        private async Task<bool> FetchIMDBStatus()
        {
            bool isUp = false;            

            string url = "https://imdb-api.com/API";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        isUp = true;

                        return isUp;
                    }
                    else
                    {
                        return isUp;
                    }
                }
                catch (Exception ex)
                {
                    return isUp;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }
    }
}
