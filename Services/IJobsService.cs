namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public interface IJobsService
    {
        Job GetJobById(string id);

        IEnumerable<Job> GetAllBySkill(string name);

        Job CreateJob(JobInputModel input);

        bool DeleteJobById(string id);
    }
}
