using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IRestaurantRepo
	{
        public Restaurant GetAPIResponse(string zipCode);
		public IEnumerable<Restaurant> GetAllRestaurants();
        //public Restaurant Restaurant(int zipCode);
        //public void InsertRestaurant(Restaurant restaurantToInsert);


    }
}

