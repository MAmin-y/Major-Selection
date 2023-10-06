using System.ComponentModel.DataAnnotations;
using MajorSelection.Enums;

namespace MajorSelection.Models.ViewModel
{
    public class CompleteUser
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName  { get; set; }

        [Required]
        [Phone]
        public string MobilePhone { get; set; }

        [Required]
        public UserSex Sex { get; set; }

        [Required]
        public int RankInTotal { get; set; }

        [Required]
        public Regions Region { get; set; }

        [Required]
        public int RankInRegion { get; set; }

        public ScholarShips ScholarShip { get; set; }
        public int RankInScholarShip { get; set; }
        public string ReportImageUrl { get; set; }
    }
}