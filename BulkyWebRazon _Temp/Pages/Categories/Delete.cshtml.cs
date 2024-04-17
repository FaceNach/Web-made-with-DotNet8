using BulkyWebRazon__Temp.Data;
using BulkyWebRazon__Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazon__Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _DB;

        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext DB)
        {
            _DB = DB;
        }
        public void OnGet(int? id)
        {
            if(id != null && id != 0)
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
            Category? UpdatedCategory = _DB.Categories.Find(Category.Id);

            if(UpdatedCategory == null)
            {
                NotFound();
            }
            else
            {
				_DB.Categories.Remove(UpdatedCategory);
				_DB.SaveChanges();

                TempData["success"] = "Category deleted succesfully";

                return RedirectToPage("Index");
			}

            return Page();
		}
    }
}
