using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cakelist.Business.Entities;
using Cakelist.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cakelist.Web.ViewComponents
{
    public class UserSelectViewComponent : ViewComponent
    {

        private readonly IUserRepository _userRepository;

        public UserSelectViewComponent(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string selectFor)
        {
            var users = _userRepository.ListAllAsync();
            var userSelectListItems = SelectListItem(await users);

            var viewModel = new UserSelectViewModel {
                Users = userSelectListItems,
                SelectFor = selectFor
            };

            return View(viewModel);
        }

        private static IEnumerable<SelectListItem> SelectListItem(IEnumerable<User> users)
        {
            return users.Select(u => new SelectListItem(u.FullName(), u.Id.ToString()));
        }
    }

    public class UserSelectViewModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public string SelectFor { get; set; }
    }
}
