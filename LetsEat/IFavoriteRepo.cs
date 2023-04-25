using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IFavoriteRepo
	{
		public IEnumerable<Restaurant> GetAllFavorites();
		public Restaurant GetRestaurant(long id);
		public void DeleteFavorite(Restaurant favorite);
		public void InsertFavorite(Restaurant restaurant);
    }
}

