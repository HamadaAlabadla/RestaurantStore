using Microsoft.AspNetCore.Mvc;

namespace RestauranteStore.Web.Controllers
{
	public class SuppliersController : Controller
	{

		//	private readonly ISupplierUserService supplierUserService;
		//	private readonly IMapper mapper;

		//	public SuppliersController(ISupplierUserService supplierUserService,
		//		IMapper mapper)
		//	{
		//		this.supplierUserService = supplierUserService;
		//		this.mapper = mapper;
		//	}


		//	public IActionResult Welcom()
		//	{
		//		return View();
		//	}

		//	// GET: SuppliersController
		//	public IActionResult Index()
		//	{
		//		return View();
		//	}

		//	[HttpPost]
		//	public async Task<IActionResult> GetAllAdmins()
		//	{

		//		var pageLength = int.Parse((Request.Form["length"].ToString()) ?? "");
		//		var skiped = int.Parse((Request.Form["start"].ToString()) ?? "");
		//		var searchData = Request.Form["search[value]"];
		//		var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
		//		var sortDir = Request.Form["order[0][dir]"];

		//		var jsonData = await supplierUserService.GetAllSupplierWithUsers(pageLength, skiped, searchData, sortColumn, sortDir);

		//		return Ok(jsonData);
		//	}


		//	// POST: SuppliersController/Create
		//	[HttpPost]
		//	public async Task<ActionResult> Create(SupplierDto supplierDto)
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			var result = await supplierUserService.CreateSupplierWithUser(supplierDto);
		//			if (result.errors.Count() == 0)
		//				return RedirectToAction(nameof(Index));
		//			else
		//			{
		//				foreach (var error in result.errors)
		//				{
		//					ModelState.AddModelError(string.Empty, error.Message ?? "");
		//				}
		//				return RedirectToAction(nameof(Index));
		//			}
		//		}
		//		else
		//		{
		//			ModelState.AddModelError(string.Empty, "Model state is inValid");
		//			return View();
		//		}
		//	}

		//	// GET: SuppliersController/Edit/5
		//	public async Task<ActionResult> Edit(int id)
		//	{
		//		var supplier = await supplierUserService.GetSupplierWithUserById(id);
		//		if (supplier == null) return NotFound();
		//		var supplierDto = mapper.Map<SupplierDto>(supplier);
		//		if (supplierDto == null) return NotFound();
		//		return View(supplierDto);
		//	}

		//	// POST: SuppliersController/Edit/5
		//	[HttpPost]
		//	public async Task<ActionResult> Edit(int id, SupplierDto supplierDto)
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			var errorModel = await supplierUserService.UpdateSupplierWithUser(supplierDto);
		//			if (errorModel.errors.Count() == 0)
		//				return RedirectToAction(nameof(Index));
		//			else
		//			{
		//				foreach (var error in errorModel.errors)
		//				{
		//					ModelState.AddModelError(string.Empty, error.Message ?? "");
		//				}
		//				return View(supplierDto);
		//			}
		//		}
		//		else
		//		{
		//			ModelState.AddModelError(string.Empty, "Model state is inValid");
		//			return View(supplierDto);
		//		}
		//	}


		//	// POST: SuppliersController/Delete/5
		//	[HttpPost]
		//	public async Task<ActionResult> Delete(int id)
		//	{
		//		var errorModel = await supplierUserService.DeleteSupplierWithUser(id);
		//		if (errorModel.errors.Count() == 0)
		//			return RedirectToAction(nameof(Index));
		//		else
		//		{
		//			foreach (var error in errorModel.errors)
		//			{
		//				ModelState.AddModelError(string.Empty, error.Message ?? "");
		//			}
		//			return RedirectToAction(nameof(Index));
		//		}
		//	}
		//}
	}
}
