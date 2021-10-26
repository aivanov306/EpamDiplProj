using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeatlesTrackDB.Data;

namespace BeatlesTracksDB.Models
{
    public class DBTracksController
    {
        public static void InsertData(IServiceProvider serviceProvider, List<ItunesTrackslist> itunesTrackslists) //
        {
            using (var context = new BeatlesTrackDBContext(serviceProvider.GetRequiredService<DbContextOptions<BeatlesTrackDBContext>>()))
            {
                foreach (var itunesTrack in itunesTrackslists)
                {
                Console.WriteLine($"Trying to insert the {itunesTrack.trackName} track contained in the {itunesTrack.collectionName} collection");
           
                
                    context.ItunesTrackslist.AddRange(
                        new ItunesTrackslist
                        {
                            trackId=itunesTrack.trackId,
                            kind = itunesTrack.kind,
                            collectionName = itunesTrack.collectionName,
                            trackName = itunesTrack.trackName,
                            collectionPrice = itunesTrack.collectionPrice,
                            trackPrice = itunesTrack.trackPrice,
                            primaryGenreName = itunesTrack.primaryGenreName,
                            trackCount = itunesTrack.trackCount,
                            trackNumber = itunesTrack.trackNumber,
                            releaseDate = itunesTrack.releaseDate,
                            artistId = itunesTrack.artistId
                            
                        }
                    );
                    
                /*
                string sqlFormattedDate = itunesTrack.releaseDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var cmd = new SqlCommand("INSERT INTO [dbo].[Tracks] VALUES (@trackId, @kind, @collectionName, @trackName, @collectionPrice, @trackPrice, @primaryGenreName, @trackCount, @trackNumber, @releaseDate)", conn);
                cmd.Parameters.AddWithValue("@trackId", itunesTrack.trackId);
                cmd.Parameters.AddWithValue("@kind", itunesTrack.kind);
                cmd.Parameters.AddWithValue("@collectionName", itunesTrack.collectionName);
                cmd.Parameters.AddWithValue("@trackName", itunesTrack.trackName);
                cmd.Parameters.AddWithValue("@collectionPrice", itunesTrack.collectionPrice);
                cmd.Parameters.AddWithValue("@trackPrice", itunesTrack.trackPrice);
                cmd.Parameters.AddWithValue("@primaryGenreName", itunesTrack.primaryGenreName);
                cmd.Parameters.AddWithValue("@trackCount", itunesTrack.trackCount);
                cmd.Parameters.AddWithValue("@trackNumber", itunesTrack.trackNumber);
                cmd.Parameters.AddWithValue("@releaseDate", sqlFormattedDate);
                cmd.ExecuteNonQuery();
                */
                }
                context.SaveChanges();
            }
        }
    }
}
