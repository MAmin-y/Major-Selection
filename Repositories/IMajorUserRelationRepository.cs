using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MajorSelection.Models.Domain;

namespace MajorSelection.Repositories
{
    public interface IMajorUserRelationRepository
    {
        Task<IEnumerable<Major>> GetAll(Guid userId);
        Task AddAsync(Guid userId, int priority, Guid majorId);
        Task<bool> DeleteAsync(Guid userId, Guid majorId);
    }
}