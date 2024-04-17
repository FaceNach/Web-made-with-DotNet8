using BulkyWebRazon__Temp.Data;
using BulkyWebRazon__Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazon__Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _DB = db;
        }
        public void OnGet()
        {
            CategoryList = _DB.Categories.ToList();
        }
    }
}
