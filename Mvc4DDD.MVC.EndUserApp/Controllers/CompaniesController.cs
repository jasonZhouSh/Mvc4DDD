﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Mvc4DDD.Application.Interfaces;
using Mvc4DDD.Domain.Entities;
using Mvc4DDD.MVC.EndUserApp.ViewModels;
using Mvc4DDD.MVC.EndUserApp.Extensions;

namespace Mvc4DDD.MVC.EndUserApp.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyAppService _companyApp;

        public CompaniesController(ICompanyAppService companyApp)
        {
            _companyApp = companyApp;
        }

        //
        // GET: /Companies/

        public ActionResult Index()
        {
            var comp = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(_companyApp.GetAll());
            //var countries = comp.Select(c => c.Country).Distinct().ToList();
            //var countries = _companyApp.GetCountries();
            //var categories = _companyApp.GetCategories();

            //ViewBag.countries = countries;
            //ViewBag.categories = categories;

            return View(comp);
        }


        //
        // GET: /Companies/Details/1/name

        public ActionResult Details(int id, String seoName)
        {
            var comp = Mapper.Map<Company, CompanyViewModel>(_companyApp.GetById(id));

            // Redirect to proper name
            if (!seoName.Equals(comp.Name.SeoString()))
                return RedirectToActionPermanent("Details", new {id = id, seoName = comp.Name.SeoString()});

            return View(comp);
        }

        /// <summary>
        /// It's receiving the original name because the database doesn't 
        /// have an ID for countries and find by SEO String on database does not
        /// have a good performance.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public ActionResult Where(String location)
        {
            var comp = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(_companyApp.GetByLocation(location));

            return View(comp);
        }

        public ActionResult What(String categoryId, String categorySeo)
        {
            var comp = Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(_companyApp.GetByCategory(categoryId));

            // Redirect to proper name
            if (!categorySeo.Equals(comp.FirstOrDefault().CategoryName.SeoString()))
                return RedirectToActionPermanent("What", new
                {
                    categoryId = categoryId,
                    categorySeo = comp.FirstOrDefault().CategoryName.SeoString()
                });

            return View(comp);
        }

        [HttpPost]
        public JsonResult GetCoords(int id)
        {
            /* This method is not useful, because the data is already on the page,
             * but I would like to test the Ajax request.
             */
            var comp = Mapper.Map<Company, CompanyViewModel>(_companyApp.GetById(id));
            return Json(new { ok = true, xcoord = comp.XCoord, ycoord = comp.YCoord });
        }

        //
        // GET: /Companies/Details/5

       /* public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Companies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Companies/Create

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

        //
        // GET: /Companies/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Companies/Edit/5

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

        //
        // GET: /Companies/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Companies/Delete/5

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
        }*/
    }
}
