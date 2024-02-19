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

        // GET: MusController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MusController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
