using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MajorSelection.Models.Domain;

namespace MajorSelection.Repositories
{
    public interface IMajorRepository
    {
        Task AddAsync(Major majorPost);
        Task<IEnumerable<Major>> GetAllAsync();
        Task<Major> GetAsync(Guid id);
        Task<Major> UpdateAsync(Major major);
        Task<bool> DeleteAsync(Guid id);
    }
}