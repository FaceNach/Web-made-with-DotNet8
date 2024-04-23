using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _DB;
        public ProductRepository(ApplicationDbContext DB) : base(DB)
        {
            _DB = DB;
        }
        public void Update(Product obj)
        {
            var ObjFromDB = _DB.Products.FirstOrDefault(u => u.Id == obj.Id);

            if (ObjFromDB != null)
            {
                ObjFromDB.Title = obj.Title;
                ObjFromDB.ISBN = obj.ISBN;
                ObjFromDB.Price = obj.Price;
                ObjFromDB.Price50 = obj.Price50;
                ObjFromDB.ListPrice = obj.ListPrice;
                ObjFromDB.Description = obj.Description;
                ObjFromDB.Category = obj.Category;
                ObjFromDB.Author = obj.Author;

                if(ObjFromDB.ImageUrl != null)
                {
                    ObjFromDB.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
