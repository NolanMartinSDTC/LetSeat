using System;
using LetsEat.Models;

namespace LetsEat
{
	public interface IFavoriteRepo
	{
		public IEnumerable<Favorite> GetAllFavorites();
        public Favorite GetFavorite(int id);
		public void DeleteFavorite(Favorite favorite);

    }
}

