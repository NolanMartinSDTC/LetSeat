using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IFavoriteRepo
	{
		public IEnumerable<Restaurant> GetAllFavorites();
		public Restaurant GetRestaurant(long id);
		public void DeleteFavorite(long id);
		public void InsertFavorite(Restaurant restaurant);
    }
}

