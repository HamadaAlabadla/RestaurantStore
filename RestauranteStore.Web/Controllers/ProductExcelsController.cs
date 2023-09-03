using Microsoft.AspNetCore.Mvc;
using RestaurantStore.Core.Dtos;
using RestaurantStore.Infrastructure.Services.ProductServices;
using RestaurantStore.Infrastructure.Services.UserService;

namespace RestaurantStore.Web.Controllers
{
    public class ProductExcelsController : Controller
    {
        private readonly IManagementProductService productService;
        private readonly IUserService userService;

        public ProductExcelsController(IManagementProductService productService,
            IUserService userService)
        {
            this.productService = productService;
            this.userService = userService;
        }


        [HttpPost]
        public IActionResult GetAllProducts()
        {

            var pageLength = int.Parse((Request.Form["length"].ToString()) ?? "");
            var skiped = int.Parse((Request.Form["start"].ToString()) ?? "");
            var searchData = Request.Form["search"];
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDir = Request.Form["order[0][dir]"];

            var jsonData = productService.GetAllProducts(pageLength, skiped, searchData, sortColumn, sortDir);
            if (jsonData == null)
            {
                var recordsTotal = 0;
                jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = new List<OrderItemDto>() };
                return Ok(jsonData);
            }
            return Ok(jsonData);
        }

        [HttpGet]
        public IActionResult AddFromExcel()
        {
            return View();
        }

        [HttpPost]
        [Route("/ProductExcels/AddExcel")]
        [RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> AddExcel(List<IFormFile> file)
        {
            if (ModelState.IsValid)
            {
                var userId = userService.GetUserByContext(HttpContext)!.Id;
                var result = await productService.AddFromFile(file.First(), HttpContext.Connection.Id, userId);
                if (result)
                    return Ok();
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: ProductExcelsController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportExcel(string search)
        {
            var result = productService.ExportExcel(search);
            if (result.Count() > 0)
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "exported_products.xlsx");
            return NotFound();
        }

        // GET: ProductExcelsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductExcelsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductExcelsController/Create
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

        // GET: ProductExcelsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductExcelsController/Edit/5
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

        // GET: ProductExcelsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductExcelsController/Delete/5
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
