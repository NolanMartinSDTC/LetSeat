using System;

namespace LetsEat.Models
{
	public class Restaurant
	{
		public Restaurant()
		{
		}

		public int ID { get; set; }
		public string Name { get; set; }
		public string CuisineType { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public bool Favorite { get; set; }
	}
}

