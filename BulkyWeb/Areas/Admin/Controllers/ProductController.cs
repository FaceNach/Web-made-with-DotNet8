using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnviroment;
        public ProductController(IUnitOfWork UnitOfWork, IWebHostEnvironment WebHostEnviroment )
        {
            _UnitOfWork = UnitOfWork;
			_WebHostEnviroment = WebHostEnviroment;

		}

        public IActionResult Index()
        {
            List<Product> ListOfProducts = _UnitOfWork.Product.GetAll(IncludeProperties:"Category").ToList();
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
                string wwwRootPath = _WebHostEnviroment.WebRootPath;

                if(file != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        //delete old image
                        var OldImagePath = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }

                    using (var FileStream = new FileStream(Path.Combine(ProductPath, FileName), FileMode.Create))
                    {
                        file.CopyTo(FileStream);
                    }

                    ProductVM.Product.ImageUrl = @"\images\product\" + FileName;
                }

                if(ProductVM.Product.Id == 0)
                {
					_UnitOfWork.Product.Add(ProductVM.Product);
				}
                else
                {
                    _UnitOfWork.Product.Update(ProductVM.Product);
                }
                
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

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ListOfProducts = _UnitOfWork.Product.GetAll(IncludeProperties: "Category").ToList();
            return Json(new { data = ListOfProducts });
        }

        #endregion
    }
}
