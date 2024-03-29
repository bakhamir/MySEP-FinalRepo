﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRazor.Abstract;
using MyRazor.Models;

namespace MyRazor.Controllers
{
    public class PhotoController : Controller
    {
        IPhoto<Photo> service;
        public PhotoController(IPhoto<Photo> service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View(service.GetPhotoAllorById("0"));
        }

        // GET: PhotoController/Details/5
        public ActionResult Details(string id)
        {
            return View(service.GetPhotoById(id));
        }

        // GET: PhotoController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Photo model)
        {

            //var status = service.AddOrEditPhoto(model);
            //if (status.status == StatusEnum.OK)
            //    return RedirectToAction("index");
            //ModelState.AddModelError("", "такое имя и расширение уже существует");
            if (model.Name != "step")
                ModelState.AddModelError("", "Вы ввели не step");
            if (model.Extension != "step")
                ModelState.AddModelError("", "Вы ввели не step");
            return View();
        }

        // GET: PhotoController/Edit/5
        public ActionResult Edit(string id)
        {
            return View(service.GetPhotoById(id));
        }

        // POST: PhotoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo model)
        {
            try
            {
                var status = service.AddOrEditPhoto(model);
                if (status.status == StatusEnum.OK)
                    return RedirectToAction("index");
                ModelState.AddModelError("Name", "Такое имя и расширение уже существуют");
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PhotoController/Delete/5
        public ActionResult Delete(string id)
        {
            
            bool status = service.DeletePhotoById(id);
            if (status  == true)
            return RedirectToAction("index");
            ModelState.AddModelError("Name", "Такое имя и расширение уже существуют");
            return View();
        }

        // POST: PhotoController/Delete/5
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
        [HttpGet]
        public ActionResult MyCheck(string name) 
        {
            if (name == "step")
                return Json(false);
            return Json(true);
                  
        }
    }
}
