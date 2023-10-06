using MajorSelection.Data;
using MajorSelection.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MajorSelection.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly MajorSelectionDbContext _MajorSelectionDbContext;

        public MajorRepository(MajorSelectionDbContext MajorSelectionDbContext)
        {
            _MajorSelectionDbContext = MajorSelectionDbContext;
        }
        public async Task AddAsync(Major major)
        {
            await _MajorSelectionDbContext.Majors.AddAsync(major);
            await _MajorSelectionDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Major>> GetAllAsync()
        {
            return await _MajorSelectionDbContext.Majors.ToListAsync();
        }
        public async Task<Major> GetAsync(Guid id)
        {
            return await _MajorSelectionDbContext.Majors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Major> UpdateAsync(Major major)
        {
            var existingMajor = await _MajorSelectionDbContext.Majors.FirstOrDefaultAsync(x => x.Id == major.Id);

            if (existingMajor != null)
            {
                existingMajor.Id = major.Id;
                existingMajor.SerialNumber = major.SerialNumber;
                existingMajor.Name = major.Name;
                existingMajor.UniversityName = major.UniversityName;
                existingMajor.Capacity = major.Capacity;
                existingMajor.Sex = major.Sex;
                existingMajor.PlaceOfDuty = major.PlaceOfDuty;
                existingMajor.Status = major.Status;
            }

            await _MajorSelectionDbContext.SaveChangesAsync();
            return existingMajor;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingMajor = await _MajorSelectionDbContext.Majors.FindAsync(id);

            if (existingMajor != null)
            {
                _MajorSelectionDbContext.Majors.Remove(existingMajor);
                await _MajorSelectionDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }


    }
}