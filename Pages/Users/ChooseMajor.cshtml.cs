using System.Security.Claims;
using MajorSelection.Models.Domain;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MajorSelection.Pages.Users
{
    public class SelectMajorModel : PageModel
    {
        private readonly IMajorUserRelationRepository majorUserRelationRepository;

        public List<Major> Majors { get; set; }

        public Guid UserId { get; set; }

        public SelectMajorModel(IMajorUserRelationRepository majorUserRelationRepository)
        {
            this.majorUserRelationRepository = majorUserRelationRepository;
        }

        public async Task OnGet(Guid userId)
        {
            UserId = userId;
            Majors = (await majorUserRelationRepository.GetAll(userId)).ToList();
        }
    }
}