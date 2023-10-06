using System.Text.Json;
using MajorSelection.Models.ViewModel;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MajorSelection.Pages.Admin.Users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IUserRepository userRepository;

        [BindProperty]
        public CompleteUser editUserRequest { get; set; }
        public EditModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task OnGet(Guid Id)
        {
            editUserRequest = await userRepository.GetAsync(Id);
            if (editUserRequest == null)
            {
                ViewData["Notification"] = new MyNotification
                    {
                        Type = MajorSelection.Enums.NotificationType.Error,
                        Message = "User Not Found!"
                    };
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userRepository.UpdateAsync(editUserRequest);
                    
                    var notification = new MyNotification
                    {
                        Type = MajorSelection.Enums.NotificationType.Success,
                        Message = "Record updated successfully!"
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                }
                catch (Exception)
                {
                    var notification = new MyNotification
                    {
                        Type = MajorSelection.Enums.NotificationType.Error,
                        Message = "Something went wrong!"
                    };
                    TempData["Notification"] = JsonSerializer.Serialize(notification);
                }
                return RedirectToPage("/admin/users/list");
            }
            return RedirectToPage("/admin/users/list");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await userRepository.DeleteAsync(editUserRequest.Id);
            if (deleted)
            {
                var notification = new MyNotification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "User was deleted successfully!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                //return RedirectToPage("/Admin/Users/List");
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}