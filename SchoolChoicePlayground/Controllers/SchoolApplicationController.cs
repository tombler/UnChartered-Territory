﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolChoicePlayground.Models;
using Microsoft.AspNet.Identity;

namespace SchoolChoicePlayground.Controllers
{
    public class SchoolApplicationController : Controller
    {
        public AppRepository Repo { get; set; }

        public SchoolApplicationController() : base() // Inherits all props and methods from base SchoolAppController
        {
            Repo = new AppRepository();
        }

        // GET: SchoolApp
        [Authorize]
        public ActionResult Index()
        {

            MyUser new_user = new Models.MyUser
            {
                email = User.Identity.GetUserName(),
                alerts = false,
                AspUser = User.Identity.GetUserId()
            };
            // This method checks whether the user is already in DB.
            Repo.AddUserToContext(new_user);
            List<School> all_schools = Repo.GetAllSchools();
            return View(all_schools);
        }

        // GET: SchoolApp/Details/5
        public ActionResult UserProfile()
        {
            string user_id = User.Identity.GetUserId();
            MyUser current_user = Repo.GetUserById(user_id);
            return View(current_user);
        }

        // GET: SchoolApp/Create
        public ActionResult SchoolProfile()
        {

            return View();
        }

        // POST: SchoolApp/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolApp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SchoolApp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolApp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SchoolApp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public string GetUserId()
        {
            string user_id = User.Identity.GetUserId();
            return user_id;
        }
    }
}
