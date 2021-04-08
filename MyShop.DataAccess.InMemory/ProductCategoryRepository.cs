using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
	public class ProductCategoryRepository
	{
		ObjectCache cache = MemoryCache.Default;
		List<ProductCategory> productCategories = new List<ProductCategory>();

		public ProductCategoryRepository()
		{
			productCategories = cache["productCategories"] as List<ProductCategory>;
			if (productCategories == null)
			{
				productCategories = new List<ProductCategory>();
			}
		}

		public void Commit()
		{
			//because we don't want to save a product right away, we do this:
			cache["productCategories"] = productCategories;
		}

		public void Insert(ProductCategory p)
		{
			productCategories.Add(p);
		}

		public void Update(ProductCategory productCategory)
		{
			ProductCategory ProductCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
			if (ProductCategoryToUpdate != null)
			{
				ProductCategoryToUpdate = productCategory;
			}
			else
			{
				throw new Exception("Product category not found");
			}
		}

		public ProductCategory Find(string Id)
		{
			ProductCategory productCategory = productCategories.Find(p => p.Id == Id);
			if (productCategory != null)
			{
				return productCategory;
			}
			else
			{
				throw new Exception("Product category not found");
			}
		}

		public IQueryable<ProductCategory> Collection()
		{
			return productCategories.AsQueryable();
		}

		public void Delete(string Id)
		{
			ProductCategory productCategorytToDelete = productCategories.Find(p => p.Id == Id);
			if (productCategorytToDelete != null)
			{
				productCategories.Remove(productCategorytToDelete);
			}
			else
			{
				throw new Exception("Product category not found");
			}
		}
	}
}
