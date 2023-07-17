using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Infrastructure.Services.ProductService;
using RestauranteStore.Infrastructure.Services.UserService;

namespace RestauranteStore.Web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService productService;
		public ProductsController(IProductService productService)
		{
			this.productService = productService;
		}
		// GET: ProductsController
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult GetAllProducts()
		{
			var jsonData = productService.GetAllProducts(Request);

			return Ok(jsonData);
		}



		// POST: ProductsController/Create
		[HttpPost]
		public async Task<ActionResult> Create(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
				var result = await productService.CreateProduct(productDto);
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return RedirectToAction(nameof(Index));
			}

		}

		// GET: ProductsController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: ProductsController/Edit/5
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

		// GET: ProductsController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ProductsController/Delete/5
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
