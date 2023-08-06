using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.Infrastructure.Services.UserService;
using RestaurantStore.Core.Dtos;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Web.Controllers
{
   
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
        [Authorize]
        public IActionResult Welcom()
        {
            return View();
        }
        // GET: AdminsController
        [Authorize(Roles = "admin")]
        public ActionResult Index(string filter = "")
        {
            var user = new UserDto()
            {
                filter = filter ?? ""
            };
            return View(user);
        }
        [Authorize]
        public ActionResult Account(string id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null) return NotFound();
            var userViewModel = mapper.Map<UserViewModel>(user);
            if (userViewModel == null) return NotFound();

            return View(userViewModel);
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditUser(string id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id) || !user.Id.Equals(id)) return NotFound();
            var userDto = mapper.Map<UserDto>(user);
            if (userDto == null) return NotFound();
            return PartialView("EditAccount", userDto);
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditEmail(string id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id) || !user.Id.Equals(id)) return NotFound();
            var emailDto = mapper.Map<EditEmailDto>(user);
            if (emailDto == null) return NotFound();
            return PartialView("EditEmail", emailDto);
        }
        [Authorize]
        [HttpGet]
        public IActionResult EditPassword(string id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id) || !user.Id.Equals(id)) return NotFound();
            var editPasswordDto = new EditPasswordDto() { Id = id };
            return PartialView("EditPassword", editPasswordDto);
        }
        [Authorize]
        [HttpGet]
        public IActionResult DeleteUser(string id)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || string.IsNullOrEmpty(user.Id) || !user.Id.Equals(id)) return NotFound();
            var deleteUserDto = new DeleteUserDto() { Id = id };
            return PartialView("DeleteUser", deleteUserDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserDto userDto)
        {
            if (userDto == null) return NotFound();
            var result = await userService.UpdateUserDetails(userDto);
            if (string.IsNullOrEmpty(result))
                return NotFound();
            else return Ok();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditEmail(EditEmailDto editEmailDto)
        {
            if (editEmailDto == null) return NotFound();
            var result = await userService.UpdateEmail(editEmailDto);
            if (string.IsNullOrEmpty(result))
                return NotFound();
            else return Ok();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordDto editPasswordDto)
        {
            if (editPasswordDto == null) return NotFound();
            var result = await userService.UpdatePassword(editPasswordDto);
            if (string.IsNullOrEmpty(result))
                return NotFound();
            else return Ok();
        }


        [Authorize]
        public IActionResult GetSupplier()
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || user.UserType != UserType.supplier) return NotFound();
            var userViewModel = mapper.Map<UserViewModel>(user);
            return Ok(userViewModel);
        }
        [Authorize]
        public IActionResult GetRestaurant()
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || user.UserType != UserType.restaurant) return NotFound();
            var userViewModel = mapper.Map<UserViewModel>(user);
            return Ok(userViewModel);
        }

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            if (userDto.UserType != UserType.restaurant)
            {
                ModelState.Remove("MainBranchName");
                ModelState.Remove("MainBranchAddress");
            }
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            
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


        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (userDto.UserType == UserType.admin)
            {
                ModelState.AddModelError("", "Not allow create Admin Account !!!");
                return LocalRedirect("/Identity/Account/Login");
            }
            if (userDto.UserType != UserType.restaurant)
            {
                ModelState.Remove("MainBranchName");
                ModelState.Remove("MainBranchAddress");
            }
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");

            if (ModelState.IsValid)
            {
                var result = await userService.CreateUser(userDto, userDto.UserType.ToString().ToLower());
                if (!string.IsNullOrEmpty(result))
                {
                    return LocalRedirect("/Identity/Account/Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error in create user");
                    return LocalRedirect("/Identity/Account/Login");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Model state is inValid");
                return LocalRedirect("/Identity/Account/Login");
            }
        }


        [Authorize(Roles = "admin")]
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
                userDto.MainBranchName = user.Restaurant!.MainBranchName;
                userDto.MainBranchAddress = user.Restaurant!.MainBranchAddress;
                if (userDto == null) return NotFound();
                return PartialView("EditRestaurant", userDto);
            }
        }
        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]

        // POST: AdminsController/Delete/5
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var errorModel = userService.DeleteUser(id);
            if (errorModel != null)
                return Ok();
            else
            {
                ModelState.AddModelError(string.Empty, "Error in delete user");
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteUser(DeleteUserDto deleteUserDto)
        {
            var user = userService.GetUserByContext(HttpContext);
            if (user == null || !user.Id.Equals(deleteUserDto.Id) || deleteUserDto == null || !deleteUserDto.isDelete) return NotFound();
            var errorModel = userService.DeleteUser(deleteUserDto.Id);
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


