using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MajorSelection.Enums;
using MajorSelection.Enums;

namespace MajorSelection.Models.Domain
{
    public class UserInformation
    {
        public Guid Id { get; set; }
        public string FullName  { get; set; }
        [Phone]
        public string MobilePhone { get; set; }
        public UserSex Sex { get; set; }
        public int RankInTotal { get; set; }
        public Regions Region { get; set; }
        public int RankInRegion { get; set; }
        public ScholarShips ScholarShip { get; set; }
        public int RankInScholarShip { get; set; }
        public string ReportImageUrl { get; set; }
    }
}