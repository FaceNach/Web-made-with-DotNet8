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
            _DB.Products.Update(obj);
        }
    }
}
