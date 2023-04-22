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
        // add list here

        private readonly IDbConnection _conn;

		public RestaurantRepo(IDbConnection conn)
		{
			_conn = conn;
		}

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return _conn.Query<Restaurant>("SELECT * FROM RESTAURANTS;");
        }

        public Restaurant GetAPIResponse(string zipCode)
        {
            var restaurant = new Restaurant();
            var client = new HttpClient();

            string key = File.ReadAllText("appsettings.json");
            string APIKey = JObject.Parse(key).GetValue("DefaultKey").ToString();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", APIKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "restaurants-near-me-usa.p.rapidapi.com");

            //Console.WriteLine("Please enter your zipcode: ");
            //var zip = Console.ReadLine();

            var APICall = $"https://restaurants-near-me-usa.p.rapidapi.com/restaurants/location/zipcode/{zipCode}/0";
            restaurant.APIResponse = client.GetStringAsync(APICall).Result;

            var formattedResponse = JArray.Parse(JObject.Parse(restaurant.APIResponse).GetValue("restaurants").ToString());

            var indexList = new List<int>();
            var random = new Random();
            bool wantMore = true;
            int rest;

            rest = random.Next(0, 10);
            while (indexList.Contains(rest) && indexList.Count < 10)
            {
                rest = random.Next(0, 10);

            }
            indexList.Add(rest);

            var index = JObject.Parse(formattedResponse[rest].ToString());
            

            restaurant.ID = (int)index.GetValue("id");
            restaurant.Name = (string)index.GetValue("restaurantName");
            restaurant.CuisineType = (string)(index.TryGetValue("cuisineType", out JToken cuisineCheck) ? cuisineCheck : "N/A");
            restaurant.Address = (string)index.GetValue("address");
            restaurant.City = (string)index.GetValue("city");
            restaurant.State = (string)index.GetValue("state");
            //restaurant.Favorite = (bool)index.GetValue("inFavorites");


            return restaurant;
        }

        //public Restaurant GetRestaurant(int zipCode)
        //{
        //    //string key = File.ReadAllText("appsettings.json");
        //    //string APIKey = JObject.Parse(key).GetValue("DefaultKey").ToString();

        //    //Console.WriteLine("Please enter your zipcode: ");
        //    //var zip = Console.ReadLine();

        //    //var client = new HttpClient();
        //    //client.DefaultRequestHeaders.Add("X-RapidAPI-Key", APIKey);
        //    //client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "restaurants-near-me-usa.p.rapidapi.com");

        //    //var restURL = $"https://restaurants-near-me-usa.p.rapidapi.com/restaurants/location/zipcode/{zip}/0";
        //    //var restResponse = client.GetStringAsync(restURL).Result;

        //    //var formattedResponse = JArray.Parse(JObject.Parse(restResponse).GetValue("restaurants").ToString());
        //    //var indexList = new List<int>();
        //    //var random = new Random();
        //    //bool wantMore = true;
        //    //int rest;
        //    ////int rest2;

        //    //rest = random.Next(0, 10);
        //    //while (indexList.Contains(rest) && indexList.Count < 10)
        //    //{
        //    //    rest = random.Next(0, 10);

        //    //}
        //    //indexList.Add(rest);

        //    ////rest2 = random.Next(0, 10);
        //    ////while (indexList.Contains(rest2) && indexList.Count < 10)
        //    ////{
        //    ////    rest2 = random.Next(0, 10);
        //    ////}
        //    ////indexList.Add(rest2);

        //    //var index = JObject.Parse(formattedResponse[rest].ToString());
        //    //var restaurant = new Restaurant();

        //    //restaurant.ID = (int)index.GetValue("id");
        //    //restaurant.Name = (string)index.GetValue("restaurantName");
        //    //restaurant.CuisineType = (string)(index.TryGetValue("cuisineType", out JToken cuisineCheck) ? cuisineCheck : "N/A");
        //    //restaurant.Address = (string)index.GetValue("address");
        //    //restaurant.City = (string)index.GetValue("city");
        //    //restaurant.State = (string)index.GetValue("state");
        //    //restaurant.Favorite = (bool)index.GetValue("inFavorites");


        //    //return restaurant;

        //    //var index2 = JObject.Parse(formattedResponse[rest2].ToString());
        //    //var name2 = index2.GetValue("restaurantName");
        //    //var cuisine2 = index2.TryGetValue("cuisineType", out JToken cuisineCheck2) ? cuisineCheck2 : "N/A";
        //    //var address2 = index2.GetValue("address");
        //    //Console.WriteLine($"Name of Restaurant: {name2}\nCuisine Type: {cuisine2}\nAddress: {address2}\n");

        //    //Console.WriteLine("Would you like to generate more options? (Y/N): ");
        //    ////var more = Console
        //    //wantMore = (Console.ReadLine().ToLower() == "y");
        //    throw new NotImplementedException();
        //}

        //public void InsertRestaurant(Restaurant restaurantToInsert)
        //{
        //    _conn.Execute("INSERT INTO restaurants (ID, NAME, CUISINE TYPE, ADDRESS, CITY, STATE, IN FAVORITES?) VALUES (@id, @name, @cuisineType, @address, @city, @state, @inFavorites);",
        //new { id = restaurantToInsert.ID, name = restaurantToInsert.Name, cuisineType = restaurantToInsert.CuisineType, address = restaurantToInsert.Address, city = restaurantToInsert.City, state = restaurantToInsert.State, inFavorites = restaurantToInsert.Favorite });
        //}

        //public Restaurant Restaurant(int zipCode)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

