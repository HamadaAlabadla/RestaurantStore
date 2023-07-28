using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Infrastructure.Services.ProductService;
using RestauranteStore.Infrastructure.Services.UserService;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Web.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService productService;
		private readonly IUserService userService;
		private readonly IMapper mapper;
		public ProductsController(IProductService productService,
			IUserService userService,
			IMapper mapper)
		{
			this.productService = productService;
			this.userService = userService;
			this.mapper = mapper;
		}
		// GET: ProductsController
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public  IActionResult GetAllProducts()
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
			if (user.UserType != UserType.supplier)
				user.Id = "admin";
			var jsonData = productService.GetAllProducts(Request, user.Id);

			return Ok(jsonData);
		}
		[HttpPost]
		public IActionResult GetAllProductsItemDto()
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
			if (user.UserType != UserType.supplier)
				user.Id = "admin";
			var jsonData = productService.GetAllProductsItemDto(Request);

			return Ok(jsonData);
		}

		[HttpGet]
		public IActionResult Add_Product()
		{
		
			return View();
		}
		//[HttpGet]
		//public IActionResult Add_Product(ProductDto productDto)
		//{
		//	return View(productDto);
		//}
		// POST: ProductsController/Create
		[HttpPost]
		public async Task<ActionResult> Create(ProductDto productDto)
		{
			if (ModelState.IsValid)
			{
				var result = await productService.CreateProduct(productDto);
				return Ok();
			}
			else
			{
				return NotFound();
			}

		}
		[HttpGet]

		// GET: ProductsController/Edit/5
		public IActionResult Edit(int id)
		{
			var product = productService.GetProduct(id);
			var productDto = mapper.Map<ProductDto>(product);
			return View(productDto);
		}

		// POST: ProductsController/Edit/5
		[HttpPost]
		public async Task<IActionResult> Edit(ProductDto productDto)
		{
			var result = await productService.UpdateProduct(productDto);
			if (result > 0)
				return Ok();
			else
				return NotFound(productDto);

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
