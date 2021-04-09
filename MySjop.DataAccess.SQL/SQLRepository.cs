using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySjop.DataAccess.SQL
{
	public class SQLRepository<T> : IRepository<T> where T : BaseEntity
	{
		internal DataContext context;
		internal DbSet<T> dbSet;

		public SQLRepository(DataContext context)
		{
			this.context = context;
			this.dbSet = context.Set<T>(); //This will set db in table, both product and productCategory
		}

		public IQueryable<T> Collection()
		{
			return dbSet;
		}

		public void Commit()
		{
			context.SaveChanges();
		}

		public void Delete(string Id)
		{
			var t = Find(Id); //Using internal find method we created
			if(context.Entry(t).State == EntityState.Detached) //Check if the object is not connected to Entity FW
				dbSet.Attach(t); //connect it, if not.
			dbSet.Remove(t); //once the object is connected to our underline entity FW we can remove it
		}

		public T Find(string Id)
		{
			return dbSet.Find(Id);
		}

		public void Insert(T t)
		{
			dbSet.Add(t);
		}

		public void Update(T t)
		{
			/*Below code tells to the FW that when we call
			 * the save method to look for the object and persist
			 * it to the DB  */
			dbSet.Attach(t);
			context.Entry(t).State = EntityState.Modified; //
		}
	}
}
