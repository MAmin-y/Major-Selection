using System.Collections.ObjectModel;
using MajorSelection.Models.Domain;
using MajorSelection.Models.ViewModel;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MajorSelection.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUserRepository userRepository;

        [BindProperty]
        public Register RegisterViewModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Email
                };

                var userInformation = new UserInformation
                {
                    FullName = RegisterViewModel.FullName,
                    MobilePhone = RegisterViewModel.MobilePhone,
                    Sex = RegisterViewModel.Sex,
                    RankInTotal = RegisterViewModel.RankInTotal,
                    Region = RegisterViewModel.Region,
                    RankInRegion = RegisterViewModel.RankInRegion,
                    ScholarShip = RegisterViewModel.ScholarShip,
                    RankInScholarShip = RegisterViewModel.RankInScholarShip,
                    ReportImageUrl = RegisterViewModel.ReportImageUrl,
                };

                var roles = new List<string>
                {
                    "User"
                };

                if (RegisterViewModel.AdminCheckbox)
                {
                    roles.Add("Admin");
                }
                
                if (await userRepository.AddAsync(identityUser, userInformation, RegisterViewModel.Password, roles))
                {
                    ViewData["Notification"] = new MyNotification
                    {
                        Type = MajorSelection.Enums.NotificationType.Success,
                        Message = "User registered successfully."
                    };
                    if (roles.Count > 1)
                    {
                        return RedirectToPage("/Admin/Users/List");
                    }
                    return RedirectToPage("/Login");
                }

                ViewData["Notification"] = new MyNotification
                {
                    Type = MajorSelection.Enums.NotificationType.Error,
                    Message = "Something went wrong."
                };

                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}
