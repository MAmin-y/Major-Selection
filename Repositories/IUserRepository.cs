using MajorSelection.Models.Domain;
using MajorSelection.Models.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace MajorSelection.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<CompleteUser>> GetAllAsync();

        Task<bool> AddAsync(IdentityUser identityUser, UserInformation userInformation, string password, List<string> roles);

        Task<bool> DeleteAsync(Guid userId);

        Task<CompleteUser> GetAsync(Guid userId);

        Task UpdateAsync(CompleteUser user);
    }
}
