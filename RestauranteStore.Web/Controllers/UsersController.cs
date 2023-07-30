using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.Infrastructure.Services.UserService;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Web.Controllers
{
	[Authorize]
	public class UsersController : Controller
	{
		private readonly IUserService userService;
		private readonly IMapper mapper;

		public UsersController(
			IMapper mapper,
			IUserService userService)
		{
			this.mapper = mapper;
			this.userService = userService;
		}

		public IActionResult Welcom()
		{
			return View();
		}
		// GET: AdminsController
		public ActionResult Index(string filter = "")
		{
			var user = new UserDto()
			{
				RestoranteDto = new RestaurantDto() { },
				filter = filter ?? ""
			};
			return View(user);
		}
		public ActionResult Account(string id)
		{
			var user = userService.GetUser(id);
			if (user == null) return NotFound();
			var userViewModel = mapper.Map<UserViewModel>(user);
			if(userViewModel == null) return NotFound();

			return View(userViewModel);
		}
		[HttpGet]
		public IActionResult EditUser(string id)
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || string.IsNullOrEmpty(user.Id) || !user.Id.Equals(id)) return NotFound();
			var userDto = mapper.Map<UserDto>(user);
			if (userDto == null) return NotFound();
			return PartialView("EditAccount" , userDto);
		}
		[HttpPost]
		public async Task<IActionResult> EditAccount(UserDto userDto)
		{
			if(userDto == null) return NotFound();
			var result = await userService.UpdateUserDetails(userDto);
			if (string.IsNullOrEmpty(result))
				return NotFound();
			else return Ok();
		}



        public IActionResult GetSupplier()
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || user.UserType != UserType.supplier) return NotFound();
			var userViewModel = mapper.Map<UserViewModel>(user);
			return Ok(userViewModel);
		}
		public IActionResult GetRestaurant()
		{
			var user = userService.GetUserByContext(HttpContext);
			if (user == null || user.UserType != UserType.restaurant) return NotFound();
			var userViewModel = mapper.Map<UserViewModel>(user);
			return Ok(userViewModel);
		}


		[HttpPost]
		public async Task<IActionResult> GetAllUsers()
		{

			var pageLength = int.Parse((Request.Form["length"].ToString()) ?? "");
			var skiped = int.Parse((Request.Form["start"].ToString()) ?? "");
			var searchData = Request.Form["search"];
			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortDir = Request.Form["order[0][dir]"];
			var filter = Request.Form["filter"];

			var jsonData = await userService.GetAllUsers(pageLength, skiped, searchData, sortColumn, sortDir, filter);

			return Ok(jsonData);
		}
		[HttpPost]
		public async Task<IActionResult> GetAllSuppliers()
		{

			var pageLength = int.Parse((Request.Form["length"].ToString()) ?? "");
			var skiped = int.Parse((Request.Form["start"].ToString()) ?? "");
			var searchData = Request.Form["search[value]"];
			var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
			var sortDir = Request.Form["order[0][dir]"];
			var filter = Request.Form["filter"];

			var jsonData = await userService.GetAllUsers(pageLength, skiped, searchData, sortColumn, sortDir, "supplier");

			return Ok(jsonData);
		}



		// POST: AdminsController/Create
		[HttpPost]
		public async Task<ActionResult> Create(UserDto userDto)
		{
			//ViewData["adminTypes"] = Enum.GetValues(typeof(UserType)).Cast<UserType>()
			//	.Select(x => new SelectListItem()
			//	{
			//		Text = x.ToString(),
			//		Value = ((int)x).ToString()
			//	}).ToList();
			if (userDto.UserType == UserType.restaurant)
				ModelState.Clear();
			if (ModelState.IsValid)
			{
				var result = await userService.CreateUser(userDto, userDto.UserType.ToString().ToLower());
				if (!string.IsNullOrEmpty(result))
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Error in create user");
					return RedirectToAction(nameof(Index));
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Model state is inValid");
				return RedirectToAction(nameof(Index));
			}
		}



		// GET: AdminsController/Edit/5
		public IActionResult Edit(string id)
		{


			var user = userService.GetUser(id);
			if (user == null) return NotFound();
			if (user.UserType != UserType.restaurant)
			{
				var userDto = mapper.Map<UserDto>(user);
				if (userDto == null) return NotFound();
				return PartialView("Edit", userDto);
			}
			else
			{
				var userDto = mapper.Map<UserDto>(user);
				userDto.RestoranteDto = mapper.Map<RestaurantDto>(user.Restaurant);
				if (userDto == null) return NotFound();
				return PartialView("EditRestaurant", userDto);
			}
		}
		// POST: AdminsController/Edit/5
		[HttpPost]

		public async Task<IActionResult> Edit(UserDto userDto)
		{

			if (ModelState.IsValid)
			{
				var errorModel = await userService.UpdateUser(userDto);
				if (!string.IsNullOrEmpty(errorModel))
					return Ok();
				else
				{
					ModelState.AddModelError(string.Empty, "Error in update user");
					return NotFound(errorModel);
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Model state is inValid");
				return NotFound(userDto);
			}
		}



		// POST: AdminsController/Delete/5
		[HttpPost]
		public async Task<ActionResult> Delete(string id)
		{
			var errorModel = await userService.DeleteUser(id);
			if (errorModel != null)
				return Ok();
			else
			{
				ModelState.AddModelError(string.Empty, "Error in delete user");
				return NotFound();
			}
		}
	}
}


