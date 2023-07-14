using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Infrastructure.Services.AdminService;
using RestauranteStore.Infrastructure.Services.UserService;
using System.Linq.Dynamic.Core;

namespace RestauranteStore.Web.Controllers
{
	[Authorize(Roles = "admin")]
	public class AdminsController : Controller
	{
		private readonly IAdminUserService adminUserService;
		private readonly IMapper mapper;

		public AdminsController(IAdminService adminService,
			IMapper mapper,
			IUserService userService,
			IAdminUserService adminUserService)
		{
			this.mapper = mapper;
			this.adminUserService = adminUserService;
		}

		public IActionResult Welcom()
		{
			return View();
		}
		// GET: AdminsController
		public ActionResult Index()
		{
			var user = new UserDto()
			{
				adminDto = new AdminDto() { Name = "hamasa" },
				supplierDto = new SupplierDto() { },
			};
			return View(user);
		}
		[HttpPost]
		public async Task<IActionResult> GetAllAdmins()
		{

			var pageLength = int.Parse((Request.Form["length"].ToString()) ?? "");
			var skiped = int.Parse((Request.Form["start"].ToString()) ?? "");
			var searchData = Request.Form["search[value]"];
			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortDir = Request.Form["order[0][dir]"];

			var jsonData = await adminUserService.GetAllAdminsWithUsers(pageLength, skiped, searchData, sortColumn, sortDir);

			return Ok(jsonData);
		}



		// POST: AdminsController/Create
		[HttpPost]
		public async Task<ActionResult> Create(AdminDto adminDto)
		{
			//ViewData["adminTypes"] = Enum.GetValues(typeof(UserType)).Cast<UserType>()
			//	.Select(x => new SelectListItem()
			//	{
			//		Text = x.ToString(),
			//		Value = ((int)x).ToString()
			//	}).ToList();
			if (ModelState.IsValid)
			{
				var result = await adminUserService.CreateAdminWithUser(adminDto);
				if (result.errors.Count() == 0)
					return RedirectToAction(nameof(Index));
				else
				{
					foreach (var error in result.errors)
					{
						ModelState.AddModelError(string.Empty, error.Message ?? "");
					}
					return View();
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Model state is inValid");
				return View();
			}
		}



		// GET: AdminsController/Edit/5
		public async Task<ActionResult> Edit(int id)
		{


			var admin = await adminUserService.GetAdminWithUserById(id);
			if (admin == null) return NotFound();
			var adminDto = mapper.Map<AdminDto>(admin);
			if (adminDto == null) return NotFound();
			return View(adminDto);
		}
		// POST: AdminsController/Edit/5
		[HttpPost]

		public async Task<ActionResult> Edit(int id, AdminDto adminDto)
		{

			if (ModelState.IsValid)
			{
				var errorModel = await adminUserService.UpdateAdminWithUser(adminDto);
				if (errorModel.errors.Count() == 0)
					return RedirectToAction(nameof(Index));
				else
				{
					foreach (var error in errorModel.errors)
					{
						ModelState.AddModelError(string.Empty, error.Message ?? "");
					}
					return View(adminDto);
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Model state is inValid");
				return View(adminDto);
			}
		}



		// POST: AdminsController/Delete/5
		[HttpPost]
		public async Task<ActionResult> Delete(int id)
		{
			var errorModel = await adminUserService.DeleteAdminWithUser(id);
			if (errorModel.errors.Count() == 0)
				return RedirectToAction(nameof(Index));
			else
			{
				foreach (var error in errorModel.errors)
				{
					ModelState.AddModelError(string.Empty, error.Message ?? "");
				}
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
