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
            var restaurant = new Restaurant();

            if (zipCode == null)
            {
                return View(restaurant);
            }
            try
            {
                restaurant = repo.GetAPIResponse(zipCode);
            }
            catch (AggregateException)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            return View(restaurant);
        }

        public IActionResult Restaurant(string zipCode)
        {
            var restaurant = new Restaurant();

            if (zipCode == null)
            {
                return View(restaurant);
            }

            restaurant = repo.GetAPIResponse(zipCode);
            return View(restaurant);
        }
    }
}

