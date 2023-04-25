using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsEat.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LetsEat.Controllers
{
    public class FavoriteController : Controller
    {
        // GET: /<controller>/
        private readonly IFavoriteRepo repo;

        public FavoriteController(IFavoriteRepo repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            var favorites = repo.GetAllFavorites();
            return View(favorites);
        }

        public IActionResult InsertFavorite(long id)
        {
            var favorite = repo.GetRestaurant(id);

            repo.InsertFavorite(favorite);
            return RedirectToAction("Index");
        }

        //public IActionResult ViewFavorite(string name)
        //{
        //    var favorite = repo.GetFavorite(name);
        //    return View(favorite);
        //}

        public IActionResult DeleteFavorite(long id)
        {
            repo.DeleteFavorite(id);
            return RedirectToAction("Index");
        }
    }
}

