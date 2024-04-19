using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        { 
            if(obj.Title == obj.Author.ToString())
            {
                ModelState.AddModelError("title", "The title can't be the same as ISBN");
            }
            if( obj != null)
            {
                _UnitOfWork.Product.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Product added succesfully";
                return RedirectToPage("Index");
            }
            return View();
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
                return RedirectToPage("Index");
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

        [HttpPost]
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
            
            return RedirectToPage("Index");
        }
    }
}
