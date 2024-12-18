using AutoMapper;
using Clothing.CMS.Application.Users;
using Clothing.CMS.Application.Users.Dto;
using Clothing.CMS.Web.Areas.Admin.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.CMS.Web.Areas.Admin.Controllers
{
	[Authorize]
	public class UserController : BaseController
	{
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public UserController(IUserService userService, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_userService = userService;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger<UserController>();
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<JsonResult> GetData()
		{
			try
			{
				var userDto = await _userService.GetAll();
				var userVM = _mapper.Map<ICollection<UserViewModel>>(userDto);

				_logger.LogInformation("Lấy ra tất cả người dùng");
				return Json(userVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		[HttpPost]
		public async Task<JsonResult> Create(CreateUserViewModel model, IFormFile? avatarURL)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("Thông tin không hợp lệ");
					return Json(new { success = false, message = "Thông tin không hợp lệ" });
				}

				var userDto = _mapper.Map<CreateUserDto>(model);
				var isSucceeded = await _userService.CreateAsync(userDto, avatarURL);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		public async Task<ActionResult> EditModal(int id)
		{
			try
			{
				var userdto = await _userService.GetById(id);
				var userVM = _mapper.Map<EditUserViewModel>(userdto);

				_logger.LogInformation($"lấy ra người dùng với ID: {id}");
				return PartialView("_EditModal", userVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return PartialView("_EditModal");
			}
		}

		[HttpPost]
		public async Task<JsonResult> Edit(EditUserViewModel model, IFormFile? avatarURL)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_logger.LogWarning("Thông tin không hợp lệ");
					return Json(new { success = false, message = "Thông tin không hợp lệ" });
				}

				var userDto = _mapper.Map<EditUserDto>(model);
				var isSucceeded = await _userService.UpdateAsync(userDto, avatarURL);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}

		public async Task<ActionResult> ViewModal(int id)
		{
			try
			{
				var userDto = await _userService.GetById(id);
				var userVM = _mapper.Map<EditUserViewModel>(userDto);

				_logger.LogInformation($"lấy ra người dùng với ID: {id}");
				return PartialView("_ViewModal", userVM);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return PartialView("_ViewModal");
			}
		}

		[HttpPost]
		public async Task<JsonResult> Delete(int id)
		{
			try
			{
				var isSucceeded = await _userService.DeleteAsync(id);
				if (!isSucceeded)
				{
					_logger.LogWarning((string?)TempData["Message"]);
					return Json(new { success = false, message = TempData["Message"] });
				}

				_logger.LogInformation((string?)TempData["Message"]);
				return Json(new { success = true, message = TempData["Message"] });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return Json(new { success = false, message = "Có lỗi xảy ra" });
			}
		}
	}
}
