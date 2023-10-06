using MajorSelection.Data;
using MajorSelection.Models.Domain;
using MajorSelection.Models.ViewModel;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MajorSelection.Pages.Admin.Majors
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly IMajorRepository _majorRepository;

        [BindProperty]
        public AddMajorRequest addMajorRequest { get; set; }

        public AddModel(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var major = new Major()
                {
                    SerialNumber = addMajorRequest.SerialNumber,
                    Name = addMajorRequest.Name,
                    UniversityName = addMajorRequest.UniversityName,
                    Capacity = addMajorRequest.Capacity,
                    Sex = addMajorRequest.Sex,
                    PlaceOfDuty = addMajorRequest.PlaceOfDuty,
                    Status = addMajorRequest.Status
                };

                await _majorRepository.AddAsync(major);

                var notification = new MyNotification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "New Major created!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Majors/List");
            }

            return Page();
        }
    }
}
