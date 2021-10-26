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
using Microsoft.Extensions.DependencyInjection;
using BeatlesTrackDB.Data;
namespace BeatlesTracksDB
{
    
    public class Program
    {
        //Initialize HTTP Client to make requests if rest client need it
        static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            await InsertDataIfNotExists(host);
            host.Run();
            
        }
        //Create DB if not exist method to automate DB creation
        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BeatlesTrackDBContext>();
                    context.Database.EnsureCreated();
                    // DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
        // If there are no data in DB query itunes REST for Beatles tracks and insert result in DB.
        private static async Task InsertDataIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BeatlesTrackDBContext>();
                if (!context.ItunesTrackslist.Any())
                {
                    try
                    {
                        var Trackslist = await ItunesRestClient.ImportItunesRestData(client);
                        DBTracksController.InsertData(services, Trackslist);

                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred seeding the DB.");
                    }
                }

            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
