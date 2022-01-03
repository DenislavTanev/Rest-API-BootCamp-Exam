namespace SoftUni_BootCamp.Models.InputModels
{
    using System.Collections.Generic;

    public class CandidateInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public RecruiterInputModel Recruiter { get; set; }
        public ICollection<SkillInputModel> Skills { get; init; } = new HashSet<SkillInputModel>();
    }
}
