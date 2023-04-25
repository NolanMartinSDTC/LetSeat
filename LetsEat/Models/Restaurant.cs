using System;

namespace LetsEat.Models
{
	public class Restaurant
	{
		public Restaurant()
		{
		}

		public string APIResponse { get; set; }
		public long RestID { get; set; }
		public string Name { get; set; }
		public string CuisineType { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }

	}
}

