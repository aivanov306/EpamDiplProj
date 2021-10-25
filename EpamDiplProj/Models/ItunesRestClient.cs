using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace BeatlesTracksDB.Models
{
    public  class ItunesRestClient
    {
        
        public static async Task ImportItunesRestData(HttpClient client)
        {
            List<ItunesTrackslist> totalItunesTrackslist = new();
            string ItunesAlbumResponseBody = await client.GetStringAsync("https://itunes.apple.com/lookup?id=136975&entity=album&limit=200");
            ItunesAlbumListRoot ItunesCollectionslist = JsonSerializer.Deserialize<ItunesAlbumListRoot>(ItunesAlbumResponseBody);
            foreach (var itunesAlbumItem in ItunesCollectionslist.results)
            {
                if (itunesAlbumItem.collectionName != null)
                {

                    string collectionNameEncoded = itunesAlbumItem.collectionName.Replace(" ", "+");
                    string TracksQueryURL = $"https://itunes.apple.com/search?limit=200&term={collectionNameEncoded}";
                    string ItunesTracksResponseBody = await client.GetStringAsync(TracksQueryURL);
                    Console.WriteLine($"Processing information for {itunesAlbumItem.collectionName}");
                    ItunesTrackslistRoot itunesRootTrackslist = JsonSerializer.Deserialize<ItunesTrackslistRoot>(ItunesTracksResponseBody);
                    List<ItunesTrackslist> itunesTrackslist = itunesRootTrackslist.results;
                    //Sometim
                    itunesTrackslist = itunesTrackslist.Where(Song => Song.collectionName == itunesAlbumItem.collectionName && Song.trackName != null && Song.artistId == 136975).ToList();
                    totalItunesTrackslist.AddRange(itunesTrackslist);
                    Console.WriteLine(itunesTrackslist.Count);
                }
            }
            Console.WriteLine(totalItunesTrackslist.Count);
        }
    }
}
