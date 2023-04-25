using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IRestaurantRepo
	{
        public List<Restaurant> GetAPIResponse(string zipCode);
        // no elements in sequence, come back to it
        public Restaurant GetRestaurant(long id);
    }
}

