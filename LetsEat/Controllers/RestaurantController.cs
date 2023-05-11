using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsEat.Models;
using Microsoft.AspNetCore.Mvc;

namespace LetsEat.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepo repo;

        public RestaurantController(IRestaurantRepo repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(string zipCode)
        {
            var restaurantList = new List<Restaurant>();

            if (zipCode == null)
            {
                return View(restaurantList);
            }
            try
            {
                restaurantList = repo.GetAPIResponse(zipCode).ToList();
            }
            catch (AggregateException)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            return View(restaurantList);
        }

        // no elements in sequence, come back to it
        // public IActionResult ViewRestaurant(long restID)
        //{
        //    var restaurant = new Restaurant();
        //    try
        //    {
        //        restaurant = repo.GetRestaurant(restID);
        //    }
        //    catch (AggregateException)
        //    {
        //        return RedirectToAction("Index", "Restaurant");
        //    }

        //    return View(restaurant);
        //}
    }
}

