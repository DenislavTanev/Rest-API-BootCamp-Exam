namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SoftUni_BootCamp.Data;
    using SoftUni_BootCamp.Data.Models;

    public class InterviewsService : IInterviewsService
    {
        private readonly ApplicationDbContext context;
        private readonly ICandidatesService candidatesService;
        private readonly IJobsService jobsService;
        private readonly ISkillsService skillsService;

        public InterviewsService(
            ApplicationDbContext context,
            ICandidatesService candidatesService,
            IJobsService jobsService,
            ISkillsService skillsService)
        {
            this.context = context;
            this.candidatesService = candidatesService;
            this.jobsService = jobsService;
            this.skillsService = skillsService;
        }

        public List<KeyValuePair<string, string>> CheckSuitableCandidates()//
        {
            var skills = this.skillsService.GetAllSkills();

            var jobs = this.context.Jobs.ToList();
            var jobSkills = this.context.JobSkills.ToList();

            var candidates = this.candidatesService.GetAllCandidates();
            var candidateSkills = this.context.CandidateSkills.ToList();

            var interviews = this.context.Interviews.ToList();

            //candidateId and jobId
            var suitableCandidates = new List<KeyValuePair<string, string>>();

            foreach (var candidate in candidates)
            {
                foreach (var job in jobs)
                {
                    foreach (var jobSkill in job.JobSkills)
                    {
                        var isCandidateApprove = candidate.CandidateSkills.Any(x => x.Skill.Id == jobSkill.SkillId);

                        if (isCandidateApprove)
                        {
                            suitableCandidates.Add(new KeyValuePair<string, string>(candidate.Id, job.Id));
                            break;
                        }
                    }
                }
            }

            return suitableCandidates;
        }

        public void CreateAvailableInterviews(Dictionary<string, string> suitableCandidates)//
        {
            //clean the candidates that alreadyhave this interview;
            var interviewFromDb = this.context.Interviews.ToList();
            foreach (var intr in interviewFromDb)
            {
                if (suitableCandidates.ContainsKey(intr.CandidateId))
                {
                    suitableCandidates.Remove(intr.CandidateId);
                }
            }

            var newInterviews = new List<Interview>();

            foreach (var pair in suitableCandidates)
            {
                var candidate = this.candidatesService.GetCandidateById(pair.Key);
                var job = this.jobsService.GetJobById(pair.Value);

                var interview = new Interview
                {
                    Candidate = candidate,
                    CandidateId = candidate.Id,
                    Job = job,
                    JobId = job.Id,
                };

                var recruiter = this.context.Recruiters.First(x => x.Id == candidate.RecruiterId);

                if (recruiter.Interviews.Count() < 5)
                {
                    recruiter.Interviews.Add(interview);
                    newInterviews.Add(interview);
                }
            }

            this.context.Interviews.AddRange(newInterviews);
            this.context.SaveChanges();
        }

        public IEnumerable<Interview> GetAllInterviews()
        {
            return this.context.Interviews.ToList();
        }
    }
}
