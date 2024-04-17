using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoryList = _dbContext.Categories.ToList();
            return View(ObjCategoryList);
        }
		public IActionResult Create()
		{
            return View();
		}

		[HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match name");
            }

            if (ModelState.IsValid)
            {
				_dbContext.Categories.Add(obj);
				_dbContext.SaveChanges();
				TempData["success"] = "Category created succesfully";
				return RedirectToAction("Index");
			}

            return View();
	    }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            Category? CategoryFromDb = _dbContext.Categories.Find(id);
			// Others ways find in DB
			//Category? CategoryFromDb1 = _dbContext.Categories.FirstOrDefault(u => u.Id == id); 
			//Category? CategoryFromDb2 = _dbContext.Categories.Where(u => u.Id == id).FirstOrDefault();

			if (CategoryFromDb == null)
            {
                return NotFound();
            }

            return View(CategoryFromDb);
        }

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_dbContext.Categories.Update(obj);
				_dbContext.SaveChanges();
				TempData["success"] = "Category updated succesfully";
				return RedirectToAction("Index");
			}

			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? CategoryFromDb = _dbContext.Categories.Find(id);
			// Others ways find in DB
			//Category? CategoryFromDb1 = _dbContext.Categories.FirstOrDefault(u => u.Id == id); 
			//Category? CategoryFromDb2 = _dbContext.Categories.Where(u => u.Id == id).FirstOrDefault();

			if (CategoryFromDb == null)
			{
				return NotFound();
			}

			return View(CategoryFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category? obj = _dbContext.Categories.Find(id);

			if(obj == null)
			{
				return NotFound();
			}
			_dbContext.Categories.Remove(obj);
			_dbContext.SaveChanges();
			TempData["success"] = "Category deleted succesfully";

			return RedirectToAction("Index");
		}
	}
}
