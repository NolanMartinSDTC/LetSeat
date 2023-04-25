using System;
using System.Data;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Dapper;
using LetsEat.Models;
using Mysqlx.Session;

namespace LetsEat
{
	public class RestaurantRepo : IRestaurantRepo
	{

        private readonly IDbConnection _conn;

		public RestaurantRepo(IDbConnection conn)
		{
			_conn = conn;
		}

        public List<Restaurant> GetAPIResponse(string zipCode)
        {
            var restaurant = new Restaurant();
            var client = new HttpClient();
            
            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key).GetValue("DefaultKey").ToString();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", APIKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "restaurants-near-me-usa.p.rapidapi.com");

            var APICall = $"https://restaurants-near-me-usa.p.rapidapi.com/restaurants/location/zipcode/{zipCode}/0";
            restaurant.APIResponse = client.GetStringAsync(APICall).Result;

            var formattedResponse = JArray.Parse(JObject.Parse(restaurant.APIResponse).GetValue("restaurants").ToString());

            var restList = new List<Restaurant>();

            for (int i = 0; i < 10; i++)
            {
                var restaurantToAdd = new Restaurant();
                var index = JObject.Parse(formattedResponse[i].ToString());

                restaurantToAdd.RestID = (long)index.GetValue("id");
                restaurantToAdd.CuisineType = (string)(index.TryGetValue("cuisineType", out JToken cuisineCheck) ? cuisineCheck : "N/A");
                restaurantToAdd.Address = (string)index.GetValue("address");
                restaurantToAdd.City = (string)index.GetValue("cityName");
                restaurantToAdd.State = (string)index.GetValue("stateName");
                restaurantToAdd.Name = (string)index.GetValue("restaurantName");

                restList.Add(restaurantToAdd);
                InsertRestaurant(restaurantToAdd);
            }
            return restList;
        }

        public void InsertRestaurant(Restaurant restaurantToInsert)
        {
            _conn.Execute("INSERT INTO restaurants (restID, NAME, CUISINETYPE, ADDRESS, CITY, STATE) VALUES (@restID, @name, @cuisineType, @address, @city, @state) " +
                "ON DUPLICATE KEY UPDATE restID = restID;",
                new { restID = restaurantToInsert.RestID, name = restaurantToInsert.Name, cuisineType = restaurantToInsert.CuisineType,
                    address = restaurantToInsert.Address, city = restaurantToInsert.City, state = restaurantToInsert.State });
        }

        //no elements in sequence, come back to it
        //public Restaurant GetRestaurant(long restID)
        //{
        //    return _conn.QuerySingle<Restaurant>("SELECT * FROM RESTAURANTS WHERE restID = @restID;",
        //        new { restID = restID });
        //}
    }
}

