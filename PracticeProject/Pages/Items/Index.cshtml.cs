using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Models;

namespace PracticeProject.Pages_Item
{
    public class IndexModel : PageModel
    {
        private readonly PracticeProject.Data.ItemContext _context;

        public IndexModel(PracticeProject.Data.ItemContext context)
        {
            _context = context;
        }

        public IList<Item> Item { get;set; }

        public async Task OnGetAsync()
        {
            Item = await _context.Items.ToListAsync();
        }
    }
}
