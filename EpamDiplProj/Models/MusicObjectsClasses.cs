using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeatlesTracksDB.Models
{
    public class ItunesAlbumList
    {
        public string collectionName { get; set; }
        public int collectionId { get; set; }
        public int trackCount { get; set; }
    }

    public class ItunesAlbumListRoot
    {
        public int resultCount { get; set; }
        public List<ItunesAlbumList> results { get; set; }
    }
    public class ItunesTrackslist
    {
        public int id { get; set; }
        public string kind { get; set; }
        public string collectionName { get; set; }
        public string trackName { get; set; }
        public double collectionPrice { get; set; }
        public double trackPrice { get; set; }
        public string primaryGenreName { get; set; }
        public int trackCount { get; set; }
        public int trackNumber { get; set; }
        public DateTime releaseDate { get; set; }
        
        public int trackId { get; set; }
        public int artistId { get; set; }

    }
    public class ItunesTrackslistRoot
    {
        public int resultCount { get; set; }
        public List<ItunesTrackslist> results { get; set; }
    }
}
