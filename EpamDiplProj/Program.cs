using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeatlesTracksDB.Models;
using System.Net.Http;
namespace BeatlesTracksDB
{
    
    public class Program
    {
        static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await ItunesRestClient.ImportItunesRestData(client);           
            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
