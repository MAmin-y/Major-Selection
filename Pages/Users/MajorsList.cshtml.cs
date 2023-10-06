using MajorSelection.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MajorSelection.Models.Domain;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using MajorSelection.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace MajorSelection.Pages.Users
{
    public class MajorsList : PageModel
    {
        private readonly IMajorRepository _majorRepository;
        public List<Major> Majors { get; set; }
        public bool IsAdmin { get; set; }

        [BindProperty]
        public Guid MajorId { get; set; }
        public Guid UserId { get; set; }


        [BindProperty]
        public int _PriorityNumber { get; set; }
        public MajorsList(IMajorRepository majorRepository)
        {
            _majorRepository = majorRepository;
        }

        public async Task OnGet(int PriorityNumber, Guid userId)
        {
            UserId = userId;
            _PriorityNumber = PriorityNumber;
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<MyNotification>(notificationJson);
            }

            Majors = (await _majorRepository.GetAllAsync())?.ToList();
        }
    }
}