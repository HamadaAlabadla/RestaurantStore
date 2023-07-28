using Microsoft.AspNetCore.Mvc;
using RestauranteStore.EF.Models;
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




		// POST: QuantitiesController/Create
		[HttpPost]
		public IActionResult Create(QuantityUnit quantityUnit)
		{
			var result = quantityService.CreateQuantity(quantityUnit);
			if (result > 0)
				return RedirectToAction(nameof(Index));
			else
				return PartialView("Add_Quantity", quantityUnit);
		}
		[HttpGet]
		// GET: QuantitiesController/Edit/5
		public IActionResult Edit(int id)
		{
			var quantity = quantityService.GetQuantity(id);
			return PartialView("Edit" ,quantity );
		}

		// POST: QuantitiesController/Edit/5
		[HttpPost]
		public IActionResult Edit(QuantityUnit quantityUnit)
		{
			var result = quantityService.UpdateQuantity(quantityUnit);
			if (result > 0)
				return Ok();
			else
				return NotFound();
		}


		// POST: QuantitiesController/Delete/5
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var result = quantityService.DeleteQuantity(id);
			if (result != null)
				return Ok();
			else return NotFound();
		}
	}
}
