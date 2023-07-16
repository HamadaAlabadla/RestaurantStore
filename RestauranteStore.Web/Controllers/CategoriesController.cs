using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Infrastructure.Services.CategoryService;

namespace RestauranteStore.Web.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly ICategoryService categoryService;

		public CategoriesController (ICategoryService categoryService)
		{
			this.categoryService = categoryService;
		}


		// GET: CategoriesController
		public ActionResult Index()
		{
			return View(categoryService.GetCategories());
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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

		// GET: CategoriesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CategoriesController/Edit/5
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

		// GET: CategoriesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CategoriesController/Delete/5
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
