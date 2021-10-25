using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeatlesTrackDB.Data;
using BeatlesTracksDB.Models;

namespace BeatlesTrackDB.Pages.Tracks
{
    public class CreateModel : PageModel
    {
        private readonly BeatlesTrackDB.Data.BeatlesTrackDBContext _context;

        public CreateModel(BeatlesTrackDB.Data.BeatlesTrackDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ItunesTrackslist ItunesTrackslist { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ItunesTrackslist.Add(ItunesTrackslist);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
