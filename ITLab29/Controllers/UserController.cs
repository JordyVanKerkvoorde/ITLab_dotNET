using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Filters;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITLab29.Controllers
{
    [ServiceFilter(typeof(LoggedOnUserFilter))]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediaRepository _mediaRepository;

        public UserController(IUserRepository userRepository, IMediaRepository mediaRepository)
        {
            _userRepository = userRepository;
            _mediaRepository = mediaRepository;
        }

        [ServiceFilter(typeof(LoggedOnUserFilter))]
        public IActionResult Index(User user)
        {
            ViewData["Avatar"] = _mediaRepository.GetAvatar(user.UserId).Path;
            ViewData["Sessions"] = _userRepository.GetRegisteredSessions(user.UserId);
            return View(user);
        }
    }
}