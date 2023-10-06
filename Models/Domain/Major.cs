using MajorSelection.Enums;

namespace MajorSelection.Models.Domain
{
    public class Major
    {
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string UniversityName { get; set; }
        public int Capacity { get; set; }
        public MajorSex Sex { get; set; }
        public string PlaceOfDuty { get; set; }
        public MajorStatus Status { get; set; }
        public Major()
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