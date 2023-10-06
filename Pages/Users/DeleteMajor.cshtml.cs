using System.Security.Claims;
using MajorSelection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MajorSelection.Pages.Users
{
    public class DeleteMajor : PageModel
    {
        public Guid UserId {set; get;}
        private readonly IMajorUserRelationRepository majorUserRelationRepository;
        public DeleteMajor(IMajorUserRelationRepository majorUserRelationRepository)
        {
            this.majorUserRelationRepository = majorUserRelationRepository;
        }

        public async Task OnGet(Guid majorId, Guid userId)
        {
            UserId= userId;
            await majorUserRelationRepository.DeleteAsync(userId, majorId);
        }
    }
}