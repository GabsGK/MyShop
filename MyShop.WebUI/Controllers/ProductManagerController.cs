using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        //By adding the parameters in this constructors I'm injecting them
        public ProductManagerController(IRepository<Product> productContex, IRepository<ProductCategory> productCategoryContext)
		{
            context = productContex;
            productCategories = productCategoryContext;
		}

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            
            return View(products);
        }

        /* First version, only pasign product
        public ActionResult Create()
		{
            Product product = new Product();
            return View(product);
        }*/
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid) {
                return View(product); //retturning page back with validation error messages
            }
			else
			{
                context.Insert(product);
                context.Commit(); //save in cache the product

                return RedirectToAction("Index"); //redirect the user to index
			}
        }

        public ActionResult Edit(string id) {
            Product product = context.Find(id);
            if (product == null)
			{
                return HttpNotFound();
			}
			else
			{
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();

                return View(viewModel);
			}
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
		{
            Product productToEdit = context.Find(id);
            if (productToEdit == null)
			{
                return HttpNotFound();
			}
            else
			{
                if (!ModelState.IsValid)
				{
                    return View(product);
				}
				else
				{
                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;

                    context.Commit();
                    return RedirectToAction("Index");
				}
			}
		}

        public ActionResult Delete(Product product, string id)
		{
            Product productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
		{
            Product productToDelete = context.Find(id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}