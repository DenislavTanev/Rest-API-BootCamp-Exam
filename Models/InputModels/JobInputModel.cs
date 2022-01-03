namespace SoftUni_BootCamp.Models.InputModels
{
    using System.Collections.Generic;

    public class JobInputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Salary { get; set; }

        public ICollection<SkillInputModel> JobSkills { get; init; } = new HashSet<SkillInputModel>();
    }
}
