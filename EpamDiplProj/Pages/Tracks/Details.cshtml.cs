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
    public class DetailsModel : PageModel
    {
        private readonly BeatlesTrackDB.Data.BeatlesTrackDBContext _context;

        public DetailsModel(BeatlesTrackDB.Data.BeatlesTrackDBContext context)
        {
            _context = context;
        }

        public ItunesTrackslist ItunesTrackslist { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ItunesTrackslist = await _context.ItunesTrackslist.FirstOrDefaultAsync(m => m.trackId == id);

            if (ItunesTrackslist == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
