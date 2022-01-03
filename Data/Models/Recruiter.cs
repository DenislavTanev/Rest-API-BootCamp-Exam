namespace SoftUni_BootCamp.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Recruiter
    {
        public Recruiter()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Interviews = new HashSet<Interview>(5);
            this.Candidates = new HashSet<Candidate>();
            this.ExperienceLevel = 1;
        }

        public string Id { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public int ExperienceLevel { get; set; }

        public ICollection<Interview> Interviews { get; set; }

        public ICollection<Candidate> Candidates { get; set; }
    }
}
