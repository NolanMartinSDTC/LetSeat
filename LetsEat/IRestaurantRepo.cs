using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IRestaurantRepo
	{
        public List<Restaurant> GetAPIResponse(string zipCode);
        public Restaurant GetRestaurant(long id);
    }
}

