using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITLab29.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMediaRepository _mediaRepository;

        public UserController(IUserRepository userRepository, UserManager<User> userManager, IMediaRepository mediaRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mediaRepository = mediaRepository;
        }

        public IActionResult Index()
        {
            //User user = _userManager.FindByIdAsync(_userManager.GetUserId(User)).Result;
            User user = _userRepository.GetById(_userManager.GetUserId(User));
            Media media = _mediaRepository.GetById(user.Avatar.MediaId);
            ViewData["MediaPath"] = media.Path;
            //ViewData["Path"] = user.Avatar==null ? "avatar null" : "avatar not null";
            return View(user);
        }
    }
}