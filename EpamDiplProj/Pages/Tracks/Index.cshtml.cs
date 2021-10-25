using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BeatlesTrackDB.Data;
using BeatlesTracksDB.Models;

namespace BeatlesTrackDB.Pages.Tracks
{
    public class IndexModel : PageModel
    {
        private readonly BeatlesTrackDB.Data.BeatlesTrackDBContext _context;

        public IndexModel(BeatlesTrackDB.Data.BeatlesTrackDBContext context)
        {
            _context = context;
        }

        public IList<ItunesTrackslist> ItunesTrackslist { get;set; }

        public async Task OnGetAsync()
        {
            ItunesTrackslist = await _context.ItunesTrackslist.ToListAsync();
        }
    }
}
