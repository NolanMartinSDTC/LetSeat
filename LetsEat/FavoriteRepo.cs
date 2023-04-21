using System;
using LetsEat.Models;
using System.Data;
using System.Collections.Generic;
using Dapper;

namespace LetsEat
{
	public class FavoriteRepo : IFavoriteRepo
	{
        private readonly IDbConnection _conn;

        public FavoriteRepo(IDbConnection conn)
        {
            _conn = conn;
        }

        public void DeleteFavorite(Favorite favorite)
        {
            _conn.Execute("DELETE FROM FAVORITES WHERE ID = @id;", new { id = favorite.ID });
        }

        public IEnumerable<Favorite> GetAllFavorites()
        {
            return _conn.Query<Favorite>("SELECT * FROM FAVORITES;");
        }

        public Favorite GetFavorite(int id)
        {
            throw new NotImplementedException();
        }
    }
}

