using BulkyWebRazon__Temp.Data;
using BulkyWebRazon__Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazon__Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext DB)
        {
            _DB = DB;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {                       
			_DB.Categories.Add(Category);
		    _DB.SaveChanges();
            TempData["success"] = "Category created succesfully";

            return RedirectToPage("Index");
        }
    }
}
