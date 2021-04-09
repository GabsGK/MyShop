using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySjop.DataAccess.SQL
{
	public class DataContext : DbContext
	{
		/*With the code below we are now able to create our data base.
		 * Entity FW Migration Classes are responsible for connecting
		 * to the DB and creating or updating the tables. We use a series
		 * of special commands executed in the command window called
		 * Package manager console */
		public DataContext()
			:base("DefaultConnection"){

		}


		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
	}
}
