using System.Collections.ObjectModel;
using MajorSelection.Data;
using MajorSelection.Models.Domain;
using MajorSelection.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MajorSelection.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;
        private readonly MajorSelectionDbContext majorSelectionDbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserRepository(AuthDbContext authDbContext,MajorSelectionDbContext majorSelectionDbContext,
            UserManager<IdentityUser> userManager)
        {
            this.authDbContext = authDbContext;
            this.userManager = userManager;
            this.majorSelectionDbContext = majorSelectionDbContext;
        }

        public async Task<bool> AddAsync(IdentityUser identityUser, UserInformation userInformation, string password, List<string> roles)
        {
            var identityResult = await userManager.CreateAsync(identityUser, password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, roles);
                if (identityResult.Succeeded)
                {
                    userInformation.Id = Guid.Parse(identityUser.Id);
                    await majorSelectionDbContext.UserInformations.AddAsync(userInformation);
                    await majorSelectionDbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            
            if (user != null)
            {
                var userInformation = await majorSelectionDbContext.UserInformations.FindAsync(Guid.Parse(user.Id));
                if (userInformation != null)
                {
                    majorSelectionDbContext.UserInformations.Remove(userInformation);
                    await majorSelectionDbContext.SaveChangesAsync();
                }
                await userManager.DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CompleteUser>> GetAllAsync()
        {
            var authUsers = await authDbContext.Users.ToListAsync();
            var superAdminUser = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "m.amin.yousefi2003@gmail.com");

            if (superAdminUser != null)
            {
                authUsers.Remove(superAdminUser);
            }

            var users = await majorSelectionDbContext.UserInformations.ToListAsync();
            List<CompleteUser> result = new List<CompleteUser>();

            Console.WriteLine(authUsers.Count() + " sdfsdfsdf " + users.Count());

            for (int i = 0; i < authUsers.Count(); i++)
            {
                result.Add(new CompleteUser
                {
                    Id = Guid.Parse(authUsers[i].Id),
                    Email = authUsers[i].Email,
                    Username = authUsers[i].UserName,
                    FullName = users[i].FullName,
                    MobilePhone = users[i].MobilePhone,
                    Sex = users[i].Sex,
                    RankInTotal = users[i].RankInTotal,
                    RankInRegion = users[i].RankInRegion,
                    RankInScholarShip = users[i].RankInScholarShip,
                    ScholarShip = users[i].ScholarShip,
                    Region = users[i].Region,
                    ReportImageUrl = users[i].ReportImageUrl
                });
            }
            return result;
        }

        public async Task<CompleteUser> GetAsync(Guid userId)
        {
            var authUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString());
            var user = await majorSelectionDbContext.UserInformations.FirstOrDefaultAsync(x => x.Id == userId);
            return new CompleteUser
            {
                Id = Guid.Parse(authUser.Id),
                Email = authUser.Email,
                Username = authUser.UserName,
                FullName = user.FullName,
                MobilePhone = user.MobilePhone,
                Sex = user.Sex,
                RankInTotal = user.RankInTotal,
                RankInRegion = user.RankInRegion,
                RankInScholarShip = user.RankInScholarShip,
                ScholarShip = user.ScholarShip,
                Region = user.Region,
                ReportImageUrl = user.ReportImageUrl
            };
        }

        public async Task UpdateAsync(CompleteUser user)
        {
            
            var existingAuthUser = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == user.Id.ToString());
            var existingUser = await majorSelectionDbContext.UserInformations
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser != null && existingAuthUser != null)
            {
                existingAuthUser.Email = user.Email;
                existingAuthUser.UserName = user.Username;
                existingUser.FullName = user.FullName;
                existingUser.MobilePhone = user.MobilePhone;
                existingUser.Sex = user.Sex;
                existingUser.RankInTotal = user.RankInTotal;
                existingUser.RankInRegion = user.RankInRegion;
                existingUser.RankInScholarShip = user.RankInScholarShip;
                existingUser.ScholarShip = user.ScholarShip;
                existingUser.Region = user.Region;
                existingUser.ReportImageUrl = user.ReportImageUrl;

            }

            await majorSelectionDbContext.SaveChangesAsync();
            await authDbContext.SaveChangesAsync();
        }
    }
}
