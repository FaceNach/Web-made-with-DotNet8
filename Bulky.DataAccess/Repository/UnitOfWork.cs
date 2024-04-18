using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _DB;
		public ICategoryRepository Category { get; private set; }

		public UnitOfWork(ApplicationDbContext DB)
        {
            _DB = DB;
			Category = new CategoryRepository(_DB);
        }

		public void Save()
		{
			_DB.SaveChanges();
		}
	}
}
