using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IFavoriteRepo
	{
		public IEnumerable<Favorite> GetAllFavorites();
        //public Restaurant GetFavorite(string name);
		public void DeleteFavorite(Restaurant favorite);
		public void InsertFavorite(Restaurant restaurant);
    }
}

