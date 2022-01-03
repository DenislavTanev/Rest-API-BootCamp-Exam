namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SoftUni_BootCamp.Data;
    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public class JobsService : IJobsService
    {
        private readonly ApplicationDbContext context;
        private readonly ISkillsService skillsService;

        public JobsService(
            ApplicationDbContext context,
            ISkillsService skillsService)
        {
            this.context = context;
            this.skillsService = skillsService;
        }

        public Job CreateJob(JobInputModel input) //
        {
            var job = new Job
            {
                Description = input.Description,
                Title = input.Title,
                Salary = input.Salary
            };

            var jobSkills = new List<JobSkill>();

            var skillInDatabase = this.skillsService.GetAllSkills();

            foreach (var skill in input.JobSkills)
            {
                if (!skillInDatabase.Any(x => x.Name == skill.Name))
                {
                    this.skillsService.CreateSkill(new SkillInputModel { Name = skill.Name });
                }
            }

            foreach (var skill in input.JobSkills)
            {
                var skillFromTheDB = this.skillsService.GetSkillByName(skill.Name);

                jobSkills.Add(new JobSkill { JobId = job.Id, SkillId = skillFromTheDB.Id });
            }


            job.JobSkills = jobSkills;
            this.context.JobSkills.AddRange(jobSkills);
            this.context.Jobs.Add(job);
            this.context.SaveChanges();

            return job;
        }

        public bool DeleteJobById(string id) //
        {
            var jobToDelete = GetJobById(id);

            if (jobToDelete == null)
            {
                return false;
            }

            var removeJobSkill = this.context.JobSkills.Where(x => x.JobId == jobToDelete.Id).ToList();

            this.context.JobSkills.RemoveRange(removeJobSkill);
            this.context.Jobs.Remove(jobToDelete);
            this.context.SaveChanges();

            return true;
        }

        public IEnumerable<Job> GetAllBySkill(string name)
        {
            var allJobs = this.context.Jobs.ToList();
            var allJobSkills = this.context.JobSkills.ToList();

            foreach (var jobS in allJobSkills)
            {
                foreach (var j in allJobs)
                {
                    if (j.Id == jobS.JobId)
                    {
                        j.JobSkills.Add(jobS);
                    }
                }
            }

            var jobsWithThatSkillName = new List<Job>();

            foreach (var job in allJobs)
            {
                foreach (var jobSkill in job.JobSkills)
                {
                    var skill = this.skillsService.GetSkillByName(name);

                    if (jobSkill.SkillId == skill.Id)
                    {
                        jobsWithThatSkillName.Add(job);
                    }
                }
            }
            return jobsWithThatSkillName;
        }

        public Job GetJobById(string id)
        {
            return this.context.Jobs.FirstOrDefault(x => x.Id == id);
        }
    }
}
