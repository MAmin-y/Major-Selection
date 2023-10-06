using System.Text.Json;
using MajorSelection.Models.ViewModel;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MajorSelection.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public List<CompleteUser> users { get; set; }
        public ListModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<MyNotification>(notificationJson);
            }

            users = (await _userRepository.GetAllAsync()).ToList();
        }

    }
}