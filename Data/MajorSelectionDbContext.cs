using MajorSelection.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MajorSelection.Data
{
    public class MajorSelectionDbContext : DbContext
    {
        public MajorSelectionDbContext(DbContextOptions<MajorSelectionDbContext> options) : base(options)
        {
        }
        public DbSet<Major> Majors { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<MajorUserRelation> MajorUserRelations { get; set; }
    }
}
