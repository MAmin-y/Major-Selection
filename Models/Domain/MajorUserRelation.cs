using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MajorSelection.Models.Domain
{
    public class MajorUserRelation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int PriorityNumber { get; set; }
        public Guid MajorId { get; set; }
    }
}