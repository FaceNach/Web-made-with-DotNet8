using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _DB;
		internal DbSet<T> DBSet;

        public Repository(ApplicationDbContext DB)
        {
            _DB = DB;
			this.DBSet = _DB.Set<T>();
        }
        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? IncludeProperties = null)
		{
			IQueryable<T> query = DBSet;
			query = query.Where(filter);

			if (!string.IsNullOrEmpty(IncludeProperties))
			{
				foreach (var IncludeProp in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(IncludeProp);
				}
			}

			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? IncludeProperties = null)
		{
			IQueryable<T> query = DBSet;

			if(!string.IsNullOrEmpty(IncludeProperties))
			{
				foreach(var IncludeProp in IncludeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(IncludeProp);
				}
			}

			return query.ToList();
		}

		public void Add(T entity)
		{
			DBSet.Add(entity);
		}

		public void Remove(T entity)
		{
			DBSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			DBSet.RemoveRange(entities);
		}
	}
}
