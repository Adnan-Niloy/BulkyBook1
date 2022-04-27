﻿using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var categoryList = _db.Categories.AsEnumerable();
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
                return View(category);
            }

            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully!";
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id.Value == 0)
                return NotFound();

            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
                return View(category);
            }

            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["success"] = "Category updated successfully!";
            return RedirectToAction("Index");

        }

        //GET
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id.Value == 0)
                return NotFound();

            var categoryFromDb = _db.Categories.Find(id);

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }

    }
}
