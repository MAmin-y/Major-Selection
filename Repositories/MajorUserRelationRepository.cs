using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MajorSelection.Data;
using MajorSelection.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MajorSelection.Repositories
{
    public class MajorUserRelationRepository : IMajorUserRelationRepository
    {
        private readonly MajorSelectionDbContext _MajorSelectionDbContext;

        public MajorUserRelationRepository(MajorSelectionDbContext MajorSelectionDbContext)
        {
            _MajorSelectionDbContext = MajorSelectionDbContext;
        }
        public async Task<IEnumerable<Major>> GetAll(Guid userId)
        {
            var Relations = await _MajorSelectionDbContext.MajorUserRelations.Where(x => x.UserId == userId).ToListAsync();
            var majors = new Major[150];
            foreach(var relation in Relations)
            {
                var major = await _MajorSelectionDbContext.Majors.FirstOrDefaultAsync(x => x.Id == relation.MajorId);
                majors[relation.PriorityNumber - 1] = major;
            }
            return majors;
        }
        public async Task AddAsync(Guid userId, int priority, Guid majorId)
        {
            var relation = new MajorUserRelation
            {
                MajorId = majorId,
                UserId = userId,
                PriorityNumber = priority
            };
            var exsitingRecord = _MajorSelectionDbContext.MajorUserRelations.FirstOrDefault(x => x.MajorId == majorId && x.UserId == userId);
            if (exsitingRecord == null)
            {
                exsitingRecord = _MajorSelectionDbContext.MajorUserRelations.FirstOrDefault(x => x.PriorityNumber == priority && x.UserId == userId);
                if(exsitingRecord != null)
                {
                    await DeleteAsync(userId, exsitingRecord.MajorId);
                }
                await _MajorSelectionDbContext.AddAsync(relation);
                await _MajorSelectionDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAsync(Guid userId, Guid majorId)
        {
            var relation = await _MajorSelectionDbContext.MajorUserRelations.FirstOrDefaultAsync(x => x.MajorId == majorId && x.UserId == userId);
            if (relation != null)
            {
                _MajorSelectionDbContext.MajorUserRelations.Remove(relation);
                await _MajorSelectionDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}