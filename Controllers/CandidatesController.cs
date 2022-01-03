namespace SoftUni_BootCamp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;
    using SoftUni_BootCamp.Services;

    [ApiController]
    [Route("[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ILogger<CandidatesController> _logger;
        private readonly ICandidatesService candidatesService;
        private readonly ICandidateSkillsService candidateSkillsService;
        private readonly IInterviewsService interviewsService;

        public CandidatesController(
            ILogger<CandidatesController> logger,
            ICandidatesService candidatesService,
            ICandidateSkillsService candidateSkillsService,
            IInterviewsService interviewsService)
        {
            _logger = logger;
            this.candidatesService = candidatesService;
            this.candidateSkillsService = candidateSkillsService;
            this.interviewsService = interviewsService;
        }

        [HttpGet]
        public IEnumerable<Candidate> Get()
        {
            return this.candidatesService.GetAllCandidates();
        }

        [HttpGet("{id}")]
        public Candidate Get(string id)
        {
            var mapCandidate = this.candidatesService.GetCandidateById(id);

            mapCandidate.CandidateSkills = this.candidateSkillsService.GetByCandidateId(id).ToList();

            return mapCandidate;
        }

        [HttpPost]
        public Candidate Post(CandidateInputModel input)
        {
            var candidate = this.candidatesService.CreateCandidate(input);

            var availableInterviews = this.interviewsService.CheckSuitableCandidates();

            var dictionary = new Dictionary<string, string>();

            foreach (var interview in availableInterviews)
            {
                dictionary.Add(interview.Key, interview.Value);
            }

            this.interviewsService.CreateAvailableInterviews(dictionary);

            return candidate;
        }

        [HttpPut("{id}")]
        public Candidate Put(string id, CandidateInputModel input)
        {
            var candidate = this.candidatesService.UpdateCandidate(id, input);
            return candidate;
        }

        [HttpDelete("{id}")]
        public string Delete(string id)
        {
            var isCandidateDeleted = this.candidatesService.DeleteCandidateById(id);


            if (!isCandidateDeleted)
            {
                return "Candidate Doesn't Exist!";

            }

            return "Candidate Is Deleted Successfully!";
        }
    }
}
