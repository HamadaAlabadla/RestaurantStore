using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Infrastructure.Services.CategoryService;
using RestauranteStore.Infrastructure.Services.QuantityService;

namespace RestauranteStore.Web.Controllers
{
	public class QuantityUnitsController : Controller
	{
		private readonly IQuantityService quantityService;

		public QuantityUnitsController(IQuantityService quantityService)
		{
			this.quantityService = quantityService;
		}


		// GET: QuantitiesController
		public ActionResult Index()
		{
			return View(quantityService.GetQuantities());
		}

		public ActionResult IndexAjax()
		{
			return Ok(quantityService.GetQuantities());
		}

		// GET: QuantitiesController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: QuantitiesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: QuantitiesController/Create
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

		// GET: QuantitiesController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: QuantitiesController/Edit/5
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

		// GET: QuantitiesController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: QuantitiesController/Delete/5
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
