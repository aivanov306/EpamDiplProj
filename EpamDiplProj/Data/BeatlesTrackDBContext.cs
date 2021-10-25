using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeatlesTracksDB.Models;

namespace BeatlesTrackDB.Data
{
    public class BeatlesTrackDBContext : DbContext
    {
        public BeatlesTrackDBContext (DbContextOptions<BeatlesTrackDBContext> options)
            : base(options)
        {
        }

        public DbSet<BeatlesTracksDB.Models.ItunesTrackslist> ItunesTrackslist { get; set; }
    }
}
