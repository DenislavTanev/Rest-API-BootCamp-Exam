namespace SoftUni_BootCamp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SoftUni_BootCamp.Data;
    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public class CandidatesService : ICandidatesService
    {
        private readonly ApplicationDbContext context;
        private readonly IRecruitersService recruitersService;
        private readonly ISkillsService skillsService;
        private readonly ICandidateSkillsService candidateSkillsService;

        public CandidatesService(
            ApplicationDbContext context,
            IRecruitersService recruitersService,
            ISkillsService skillsService,
            ICandidateSkillsService candidateSkillsService)
        {
            this.context = context;
            this.recruitersService = recruitersService;
            this.skillsService = skillsService;
            this.candidateSkillsService = candidateSkillsService;
        }

        public Candidate CreateCandidate(CandidateInputModel input)
        {
            var newCandidate = new Candidate
            {
                Description = input.Description,
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
            };

            var recruiter = recruitersService.GetRecruiterByName(input.Recruiter.LastName);

            if (recruiter == null)
            {
                recruiter = recruitersService.CreateRecruiter(input.Recruiter);
            }
            else
            {
                recruiter.ExperienceLevel += 1;
            }

            newCandidate.RecruiterId = recruiter.Id;

            var newSkills = new List<Skill>();

            var skills = this.skillsService.GetAllSkills();

            foreach (var skill in input.Skills)
            {
                if (!skills.Any(x => x.Name == skill.Name))
                {
                    this.skillsService.CreateSkill(skill);
                }
            }

            this.context.Candidates.Add(newCandidate);
            this.context.SaveChanges();

            foreach (var skill in input.Skills)
            {
                var skillFromDb = this.skillsService.GetSkillByName(skill.Name);

                var newSkill = this.candidateSkillsService.CreateCandidateSkill(newCandidate.Id, skillFromDb);

                newCandidate.CandidateSkills.Add(newSkill);
            }

            return newCandidate;
        }

        public bool DeleteCandidateById(string candidateId)
        {
            var candidateToDelete = GetCandidateById(candidateId);

            if (candidateToDelete == null)
            {
                return false;
            }

            var removeCandidateSkill = this.candidateSkillsService.GetByCandidateId(candidateId); 

            this.context.CandidateSkills.RemoveRange(removeCandidateSkill);
            this.context.Candidates.Remove(candidateToDelete);
            this.context.SaveChanges();

            return true;
        }

        public IEnumerable<Candidate> GetAllCandidates()
        {
            var allCandidates = this.context.Candidates.ToList();

            return allCandidates;
        }

        public Candidate GetCandidateById(string candidateId)
        {
            var candidateById = this.context.Candidates.FirstOrDefault(x => x.Id == candidateId);

            return candidateById;
        }

        public Recruiter GetCandidateRecruiterById(string candidateRecruiterId)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailAvailable(string email)
        {
            return this.context.Candidates.Any(x => x.Email == email);
        }

        public bool IsFirstNameAvailable(string firstName)
        {
            return this.context.Candidates.Any(x => x.FirstName == firstName);
        }

        public bool IsLastNameAvailable(string lastName)
        {
            return this.context.Candidates.Any(x => x.LastName == lastName);
        }

        public Candidate UpdateCandidate(string id, CandidateInputModel input)
        {
            var candidate = GetCandidateById(id);

            if (candidate == null)
            {
                return null;
            }
            candidate.Description = input.Description;
            candidate.Email = input.Email;
            candidate.FirstName = input.FirstName;
            candidate.LastName = input.LastName;

            var recruiter = this.recruitersService.GetRecruiterByName(input.Recruiter.LastName);

            if (recruiter == null)
            {
                this.recruitersService.CreateRecruiter(input.Recruiter);
            }
            else
            {
                recruiter.ExperienceLevel += 1;
            }

            candidate.RecruiterId = recruiter.Id;

            foreach (var skill in input.Skills)
            {
                this.skillsService.CreateSkill(skill);
            }

            this.RemoveCandidateSkills(candidate.Id);

            foreach (var skill in input.Skills)
            {
                var skillFromDb = this.skillsService.GetSkillByName(skill.Name);

                var newSkill = this.candidateSkillsService.CreateCandidateSkill(candidate.Id, skillFromDb);

                candidate.CandidateSkills.Add(newSkill);
            }


            this.context.Candidates.Update(candidate);

            this.context.SaveChanges();
            return candidate;
        }

        public void RemoveCandidateSkills(string candidateId)
        {
            var candidateSkills = this.candidateSkillsService.GetByCandidateId(candidateId);

            this.context.RemoveRange(candidateSkills);
            this.context.SaveChanges();
        }
    }
}
