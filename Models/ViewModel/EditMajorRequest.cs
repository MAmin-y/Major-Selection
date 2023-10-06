using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MajorSelection.Enums;

namespace MajorSelection.Models.ViewModel
{
    public class EditMajorRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int SerialNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UniversityName { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public MajorSex Sex { get; set; }
        [Required]
        public string PlaceOfDuty { get; set; }
        [Required]
        public MajorStatus Status { get; set; }
        public EditMajorRequest()
        {
            if(Name == null)
            {
                Name = "";
            }
            if(UniversityName == null)
            {
                UniversityName = "";
            }
            if(PlaceOfDuty == null)
            {
                PlaceOfDuty = "";
            }
        }
        
    }
}