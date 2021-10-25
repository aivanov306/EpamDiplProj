using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeatlesTrackDB.Data;
using BeatlesTracksDB.Models;

namespace BeatlesTrackDB.Pages.Tracks
{
    public class EditModel : PageModel
    {
        private readonly BeatlesTrackDB.Data.BeatlesTrackDBContext _context;

        public EditModel(BeatlesTrackDB.Data.BeatlesTrackDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ItunesTrackslist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItunesTrackslistExists(ItunesTrackslist.trackId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItunesTrackslistExists(int id)
        {
            return _context.ItunesTrackslist.Any(e => e.trackId == id);
        }
    }
}
