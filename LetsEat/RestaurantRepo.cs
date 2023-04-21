using System;
using System.Data;
using System.Collections.Generic;
using Dapper;
using LetsEat.Models;

namespace LetsEat
{
	public class RestaurantRepo : IRestaurantRepo
	{
		private readonly IDbConnection _conn;

		public RestaurantRepo(IDbConnection conn)
		{
			_conn = conn;
		}

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _conn.Query<Restaurant>("SELECT * FROM RESTAURANTS;");
        }

        public Restaurant GetRestaurant(int id)
        {
            return _conn.QuerySingle<Restaurant>("SELECT * FROM RESTAURANTS WHERE ID = @id", new { id = id });
        }
    
    }
}

