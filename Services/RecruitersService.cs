namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SoftUni_BootCamp.Data;
    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public class RecruitersService : IRecruitersService
    {
        private readonly ApplicationDbContext context;

        public RecruitersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Recruiter CreateRecruiter(RecruiterInputModel input)
        {
            var recruiter = new Recruiter
            {
                LastName = input.LastName,
                Email = input.Email,
                Country = input.Country,
            };

            this.context.Recruiters.Add(recruiter);

            return recruiter;
        }

        public IEnumerable<Recruiter> GetAllRecruites()
        {
            var allRecruiters = this.context.Recruiters.ToList();

            var recruitersWithCandidateSkills = this.GetRecruitersListAllCandidateSkills(allRecruiters);

            return recruitersWithCandidateSkills;
        }

        public IEnumerable<Recruiter> GetRecruitersByLevel(int level)
        {
            var recruiters = this.context.Recruiters
                .Where(x => x.ExperienceLevel == level)
                .ToList();

            var recruitersWithCandidateSkills = this.GetRecruitersListAllCandidateSkills(recruiters);

            return recruitersWithCandidateSkills;
        }

        public Recruiter GetRecruiterByName(string lastName)
        {
            var recruiter = this.context.Recruiters
                .FirstOrDefault(x => x.LastName == lastName);

            return recruiter;
        }

        private IEnumerable<Recruiter> GetRecruitersListAllCandidateSkills(List<Recruiter> recruiters)
        {
            foreach (var recruiter in recruiters)
            {
                recruiter.Candidates = this.context.Candidates.Where(x => x.RecruiterId == recruiter.Id).ToList();

                foreach (var candidate in recruiter.Candidates)
                {
                    candidate.CandidateSkills = this.context.CandidateSkills.Where(x => x.CandidateId == candidate.Id).ToList();

                    foreach (var candidateSkill in candidate.CandidateSkills)
                    {
                        candidateSkill.Skill = this.context.Skills.FirstOrDefault(x => x.Id == candidateSkill.SkillId);
                    }
                }
            }

            return recruiters;
        }
    }
}
