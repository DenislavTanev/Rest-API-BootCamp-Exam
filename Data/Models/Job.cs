namespace SoftUni_BootCamp.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Job
    {
        public Job()
        {
            this.Id = Guid.NewGuid().ToString();
            this.JobSkills = new HashSet<JobSkill>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Salary { get; set; }

        public ICollection<JobSkill> JobSkills { get; set; }
    }
}
