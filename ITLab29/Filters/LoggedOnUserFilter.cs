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
using ITLab29.Exceptions;

namespace ITLab29.Filters {
    public class LoggedOnUserFilter : ActionFilterAttribute{
        private readonly IUserRepository _userRepository;

        public LoggedOnUserFilter(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            //_user = _userRepository.GetById(context.HttpContext.User.Identity.Name);
            try
            {
                context.ActionArguments["user"] = context.HttpContext.User.Identity.IsAuthenticated ?
                _userRepository.GetByName(context.HttpContext.User.Identity.Name) : null;
            }
            catch (UserNotFoundException)
            {
                context.ActionArguments["user"] = null;
            }
            base.OnActionExecuting(context);
        }
    }
}
