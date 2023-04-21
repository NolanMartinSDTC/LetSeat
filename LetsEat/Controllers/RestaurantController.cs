using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            var restaurants = repo.GetAllRestaurants();
            return View(restaurants);
        }

        public IActionResult ViewRestaurant(int id)
        {
            var restaurant = repo.GetRestaurant(id);
            return View(restaurant);
        }
    }
}

