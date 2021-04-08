using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
	//we only want to implement it, never change it, that is why is abstract.
	public abstract class BaseEntity
	{
		public string Id { get; set; }
		public DateTimeOffset CreatedAt { get; set; }


		//Below the constructor, in where we generate the id everytime a new object is created.
		public BaseEntity()
		{
			this.Id = Guid.NewGuid().ToString();
			this.CreatedAt = DateTime.Now;
		}

	}
}
