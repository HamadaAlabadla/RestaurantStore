using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.OrderService;
using RestauranteStore.Infrastructure.Services.ProductService;
using RestauranteStore.Infrastructure.Services.UserService;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public ProductsController(IProductService productService,
            IUserService userService,
            IMapper mapper,
            IOrderService orderService)
        {
            this.productService = productService;
            this.userService = userService;
            this.mapper = mapper;
            this.orderService = orderService;
        }
        // GET: ProductsController
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "admin , supplier")]
        [HttpPost]
        public IActionResult GetAllProducts()
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            if (user.UserType != UserType.supplier)
                user.Id = "admin";
            var jsonData = productService.GetAllProducts(Request, user.Id);

            return Ok(jsonData);
        }
        [Authorize(Roles = "restaurant")]
        [HttpPost]
        public async Task<IActionResult> GetAllProductsItemDto(int id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id)) return NotFound();
            if (user.UserType != UserType.supplier)
                user.Id = "admin";
            var order = await orderService.GetOrder(id, user.Id);
            var supplierId = (order ?? new Order() { SupplierId = "" }).SupplierId;
            var jsonData = productService.GetAllProductsItemDto(Request, supplierId, user.Id);

            return Ok(jsonData);
        }
        [Authorize(Roles = "supplier")]
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
        [Authorize(Roles = "supplier")]
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
        [Authorize(Roles = "supplier")]
        [HttpGet]

        // GET: ProductsController/Edit/5
        public IActionResult Edit(int id)
        {
            var product = productService.GetProduct(id);
            var productDto = mapper.Map<ProductDto>(product);
            return View(productDto);
        }
        [Authorize(Roles = "supplier")]
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

        [Authorize(Roles = "admin , supplier")]
        // POST: ProductsController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = productService.GetProduct(id);
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || product == null || ((user.UserType != UserType.admin) ? !product.UserId.Equals(user.Id) : false))
                return NotFound();
            var result = productService.DeleteProduct(id);
            if (result == null)
                return NotFound();
            else return Ok();
        }
    }
}
