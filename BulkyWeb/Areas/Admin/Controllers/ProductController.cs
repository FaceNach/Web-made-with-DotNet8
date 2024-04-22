using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> ListOfProducts = _UnitOfWork.Product.GetAll().ToList();
            return View(ListOfProducts);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM ProductVM = new ProductVM()
            {
                 CategoryList = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem
				 {
					 Text = u.Name,
					 Value = u.Id.ToString()
				 }),   
                 Product = new Product()
            };

            if(id == null || id == 0 )
            {
                return View(ProductVM);
            }
            else
            {
                ProductVM.Product = _UnitOfWork.Product.Get(u => u.Id == id);
                return View(ProductVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM ProductVM, IFormFile? file)
        {  
            if( ModelState.IsValid)
            {
                _UnitOfWork.Product.Add(ProductVM.Product);
                _UnitOfWork.Save();
                TempData["success"] = "Product added succesfully";
                return RedirectToAction("Index");
            }
            else
            {

                ProductVM.CategoryList = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(ProductVM);
			}
        }

        public IActionResult Delete(int? id)
        {
            if( id == null || id == 0)
            {
                NotFound();
            }

            Product ProductFromDB = _UnitOfWork.Product.Get(u => u.Id == id);

            if (ProductFromDB == null)
            {
                NotFound();
            }
            return View(ProductFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _UnitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                NotFound();
            }
            
            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
   
        }
    }
}
