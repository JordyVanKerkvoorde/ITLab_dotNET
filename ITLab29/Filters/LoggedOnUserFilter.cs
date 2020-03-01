using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ITLab29.Filters {
    public class LoggedOnUserFilter : ActionFilterAttribute{
        private readonly IUserRepository _userRepository;
        private User _user;

        public LoggedOnUserFilter(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            context.ActionArguments["user"] = context.HttpContext.User.Identity.IsAuthenticated ?
                _userRepository.GetById(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}
