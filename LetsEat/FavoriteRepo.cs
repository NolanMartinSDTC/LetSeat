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
            _conn.Execute("DELETE FROM FAVORITES WHERE restID = @id;", new { id = favorite.ID });
        }

        public void InsertFavorite(Restaurant restaurantToInsert)
        {
            _conn.Execute("INSERT INTO favorites (restID, NAME, CUISINE, ADDRESS, CITY, STATE) VALUES (@id, @name, @cuisineType, @address, @city, @state) " +
                "ON DUPLICATE KEY UPDATE restID = restID;",
                new
                {
                    id = restaurantToInsert.ID,
                    name = restaurantToInsert.Name,
                    cuisineType = restaurantToInsert.CuisineType,
                    address = restaurantToInsert.Address,
                    city = restaurantToInsert.City,
                    state = restaurantToInsert.State
                });
        }
        public IEnumerable<Favorite> GetAllFavorites()
        {
            return _conn.Query<Favorite>("SELECT * FROM FAVORITES;");
        }

        //public Restaurant GetFavorite(string name)
        //{
        //    throw new NotImplementedException();
        //}
    }   
}

