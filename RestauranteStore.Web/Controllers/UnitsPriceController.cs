using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Infrastructure.Services.UnitPriceService;

namespace RestauranteStore.Web.Controllers
{
	public class UnitsPriceController : Controller
	{
		private readonly IUnitPriceService unitPriceService;
		public UnitsPriceController(IUnitPriceService unitPriceService)
		{
			this.unitPriceService = unitPriceService;
		}
		// GET: UnitsPriceController
		public ActionResult Index()
		{
			return View(unitPriceService.GetUnitsPrice());
		}

		public ActionResult IndexAjax()
		{
			return Ok(unitPriceService.GetUnitsPrice());
		}

		// GET: UnitsPriceController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: UnitsPriceController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UnitsPriceController/Create
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

		// GET: UnitsPriceController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: UnitsPriceController/Edit/5
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

		// GET: UnitsPriceController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: UnitsPriceController/Delete/5
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
