using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsEat.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LetsEat.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: /<controller>/
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
                restaurantList = repo.GetAPIResponse(zipCode);
            }
            catch (AggregateException)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            return View(restaurantList);
        }

        public IActionResult ViewRestaurant (string name)
        {
            var restaurant = new Restaurant();
            try
            {
                restaurant = repo.GetRestaurant(name);
            }
            catch (AggregateException)
            {
                return RedirectToAction("Index", "Restaurant");
            }

            return View(restaurant);
        }
    }
}

