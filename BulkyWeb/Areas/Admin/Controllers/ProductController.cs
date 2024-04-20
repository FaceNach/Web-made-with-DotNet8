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

        public IActionResult Create()
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

            return View(ProductVM);  
        }

        [HttpPost]
        public IActionResult Create(ProductVM ProductVM)
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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }

            Product ProductFromDB = _UnitOfWork.Product.Get(u => u.Id == id);

            if (ProductFromDB == null)
            {
                NotFound();
            }
            
            return View (ProductFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if(ModelState.IsValid)
            {
                _UnitOfWork.Product.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }

            return View();
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
