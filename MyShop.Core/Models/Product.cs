using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
	public class Product
	{
		public string Id { get; set; }

		[StringLength(20)]
		[DisplayName("Product Name")]
		public string Name { get; set; }
		
		public string Description { get; set; }
		
		[Range(9, 1000)] //to avoid unreasonable prices, jeje
		public decimal Price { get; set; }
		
		public string Category { get; set; }
		
		public string Image { get; set; }

		public Product()
		{
			this.Id = Guid.NewGuid().ToString();
		}
	}
}
