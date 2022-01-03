namespace SoftUni_BootCamp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;
    using SoftUni_BootCamp.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsService jobsService;
        private readonly IInterviewsService interviewsService;

        public JobsController(
            IJobsService jobsService,
            IInterviewsService interviewsService)
        {
            this.jobsService = jobsService;
            this.interviewsService = interviewsService;
        }

        [HttpGet("{id}")]
        public Job Get(string id)
        {
            var job = this.jobsService.GetJobById(id);
            return job;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> All(string name)
        {
            var jobs = this.jobsService.GetAllBySkill(name).ToList();
            return jobs;
        }

        [HttpPost]
        public Job Post(JobInputModel input)
        {
            var newJob = this.jobsService.CreateJob(input);

            var availableInterviews = this.interviewsService.CheckSuitableCandidates();

            var dictionary = new Dictionary<string, string>();

            foreach (var interview in availableInterviews)
            {
                dictionary.Add(interview.Key, interview.Value);
            }

            this.interviewsService.CreateAvailableInterviews(dictionary);

            return newJob;
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var isJobDeleted = this.jobsService.DeleteJobById(id);
            if (!isJobDeleted)
            {
                return $"We don't have job with this ID {id} ";
            }

            return "Job Successfully Deleted!";
        }
    }
}
