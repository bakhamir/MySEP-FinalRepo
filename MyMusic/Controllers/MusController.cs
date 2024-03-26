using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Abstract;
using MyMusic.Models;
using System.Security.Claims;

namespace MyMusic.Controllers
{
    [Authorize]
    public class MusController : Controller
    {
        IMusic<song> service;
        public MusController(IMusic<song> service)
        {
            this.service = service;
        }
        // GET: MusController   
        public ActionResult Index()
        {
            ViewData["role"] = User.FindFirstValue(ClaimTypes.Role);
            ViewBag.id = User.FindFirstValue("id");
            return View(service.GetValues());
        }

        // GET: MusController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MusController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(song song)
        {
            bool res = service.AddSong(song);
            if (res == true)
            {
   
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var song = service.GetSongById(id); 
            return View(song);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(song song)
        {
            bool res = service.EditSong(song);
            if (res)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(song);
            }
        }

        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var song = service.GetSongById(id); 
            return View(song);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            bool res = service.DeleteSong(id);
            if (res)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

    }
}
