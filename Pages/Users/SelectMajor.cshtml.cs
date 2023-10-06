
using System.Security.Claims;

using MajorSelection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MajorSelection.Pages.Users
{
    public class SelectMajor : PageModel
    {
        public Guid UserId {set; get;}
        private readonly IMajorUserRelationRepository majorUserRelationRepository;
        public SelectMajor(IMajorUserRelationRepository majorUserRelationRepository)
        {
            this.majorUserRelationRepository = majorUserRelationRepository;
        }

        public async Task OnGet(int PriorityNumber, Guid MajorId, Guid userId)
        {
            await majorUserRelationRepository.AddAsync(
                userId, PriorityNumber, MajorId);
            UserId = userId;
        }
    }
}