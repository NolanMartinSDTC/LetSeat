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

        public void DeleteFavorite(Restaurant favorite)
        {
            _conn.Execute("DELETE FROM FAVORITES WHERE restID = @id;", new { id = favorite.RestID });
        }

        public void InsertFavorite(Restaurant restaurantToInsert)
        {
            _conn.Execute("INSERT INTO FAVORITES (RESTID, NAME, CUISINETYPE, ADDRESS, CITY, STATE) VALUES (@id, @name, @cuisine, @address, @city, @state) " +
                "ON DUPLICATE KEY UPDATE restID = restID;",
                new
                {
                    id = restaurantToInsert.RestID,
                    name = restaurantToInsert.Name,
                    cuisine = restaurantToInsert.CuisineType,
                    address = restaurantToInsert.Address,
                    city = restaurantToInsert.City,
                    state = restaurantToInsert.State
                });
        }
        public IEnumerable<Restaurant> GetAllFavorites()
        {
            return _conn.Query<Restaurant>("SELECT * FROM FAVORITES;");
        }

        public Restaurant GetRestaurant(long id)
        {
            return _conn.Query<Restaurant>("SELECT * FROM RESTAURANTS WHERE RESTID = @id;", new { id = id }).ToList()[0];
        }
    }   
}

