namespace SoftUni_BootCamp.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Candidate
    {
        public Candidate()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CandidateSkills = new HashSet<CandidateSkill>();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string RecruiterId { get; set; }


        public ICollection<CandidateSkill> CandidateSkills { get; set; }
    }
}
