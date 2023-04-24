using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IRestaurantRepo
	{
        public List<Restaurant> GetAPIResponse(string zipCode);
		public IEnumerable<Restaurant> GetAllRestaurants();
        public Restaurant GetRestaurant(string name);


    }
}

