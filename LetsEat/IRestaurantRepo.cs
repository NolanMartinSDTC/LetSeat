﻿using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IRestaurantRepo
	{
		public IEnumerable<Restaurant> GetAllRestaurants();
		public Restaurant GetRestaurant(int id);

	}
}

