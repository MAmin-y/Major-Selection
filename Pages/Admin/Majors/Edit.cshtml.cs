using MajorSelection.Models.Domain;
using MajorSelection.Models.ViewModel;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace MajorSelection.Pages.Admin.Majors
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IMajorRepository _majorRepository;

        [BindProperty]
        public EditMajorRequest Major { get; set; }

        public EditModel(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        public async Task OnGet(Guid id)
        {
            var majorDomainModel = await _majorRepository.GetAsync(id);

            if (majorDomainModel != null)
            {
                Major = new EditMajorRequest
                {
                    Id = majorDomainModel.Id,
                    SerialNumber = majorDomainModel.SerialNumber,
                    Name = majorDomainModel.Name,
                    UniversityName = majorDomainModel.UniversityName,
                    Capacity = majorDomainModel.Capacity,
                    Sex = majorDomainModel.Sex,
                    PlaceOfDuty = majorDomainModel.PlaceOfDuty,
                    Status = majorDomainModel.Status,
                };
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            ValidateEditMajor();

            if (ModelState.IsValid)
            {
                try
                {
                    var majorDomainModel = new Major
                    {
                        Id = Major.Id,
                        SerialNumber = Major.SerialNumber,
                        Name = Major.Name,
                        UniversityName = Major.UniversityName,
                        Capacity = Major.Capacity,
                        Sex = Major.Sex,
                        PlaceOfDuty = Major.PlaceOfDuty,
                        Status = Major.Status,
                    };


                    await _majorRepository.UpdateAsync(majorDomainModel);

                    ViewData["Notification"] = new MyNotification
                    {
                        Type = Enums.NotificationType.Success,
                        Message = "Record updated successfully!"
                    };
                }
                catch (Exception)
                {
                    ViewData["Notification"] = new MyNotification
                    {
                        Type = Enums.NotificationType.Error,
                        Message = "Something went wrong!"
                    };
                }

                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _majorRepository.DeleteAsync(Major.Id);
            if (deleted)
            {
                var notification = new MyNotification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Major was deleted successfully!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Majors/List");
            }

            return Page();
        }


        private void ValidateEditMajor()
        {
            if (!string.IsNullOrWhiteSpace(Major.Name))
            {
                // check for minimum length
                if (Major.Name.Length > 72)
                {
                    ModelState.AddModelError("Major.Name",
                        "Name can only be less 72 characters.");
                }
                // check for maximum length
            }
        }
    }
}
