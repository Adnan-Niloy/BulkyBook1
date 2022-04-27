﻿using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public IActionResult Index()
        {
            var categoryList = _categoryRepository.GetAll();
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

            _categoryRepository.Add(category);
            _categoryRepository.Save();
            TempData["success"] = "Category created successfully!";
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id.Value == 0)
                return NotFound();

            var categoryFromDb = _categoryRepository.GetFirstOrDefault(c => c.Id == id);

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

            _categoryRepository.Update(category);
            _categoryRepository.Save();
            TempData["success"] = "Category updated successfully!";
            return RedirectToAction("Index");

        }

        //GET
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id.Value == 0)
                return NotFound();

            var categoryFromDb = _categoryRepository.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _categoryRepository.GetFirstOrDefault(c => c.Id == id);

            if (category == null)
                return NotFound();

            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }

    }
}
