using Microsoft.AspNetCore.Mvc;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.CategoryService;

namespace RestauranteStore.Web.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ICategoryService categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}


		// GET: CategoriesController
		public ActionResult Index()
		{
			return View(categoryService.GetCategories());
		}

		// GET: CategoriesController
		public IActionResult IndexAjax()
		{
			return Ok(categoryService.GetCategories());
		}


		// POST: CategoriesController/Create
		[HttpPost]
		public IActionResult Create(Category category)
		{
			var result = categoryService.CreateCategory(category);
			if (result > 0)
				return RedirectToAction(nameof(Index));
			else
				return PartialView("Add_Cat", category);
		}
		[HttpGet]
		// GET: CategoriesController/Edit/5
		public IActionResult Edit(int id)
		{
			var cat = categoryService.GetCategory(id);
			return PartialView("Edit", cat);
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		public IActionResult Edit(Category category)
		{
			var result = categoryService.UpdateCategory(category);
			if (result > 0)
				return Ok();
			else
				return NotFound(category);
		}


		// POST: CategoriesController/Delete/5
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var result = categoryService.DeleteCategory(id);
			if (result != null)
				return Ok();
			else
				return NotFound();
		}
	}
}
