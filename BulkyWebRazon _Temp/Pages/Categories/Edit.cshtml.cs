using BulkyWebRazon__Temp.Data;
using BulkyWebRazon__Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazon__Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _DB;

        [BindProperty]
        public Category Category { get; set; }

        [BindProperty]
        public int CategoryID { get; set; }

        public EditModel(ApplicationDbContext DB)
        {
            _DB = DB; 
        }
        public void OnGet(int? id)
        {
            if(id != null && id !=0)
            {
                Category = _DB.Categories.Find(id);
			}
            else
            {
                NotFound();
            }
        }

        public IActionResult OnPost()
        {     
            if (Category != null)
            {
				_DB.Categories.Update(Category);
				_DB.SaveChanges();
                TempData["success"] = "Category updated succesfully";

                return RedirectToPage("Index");
			}

			return Page();
		}
    }
}
