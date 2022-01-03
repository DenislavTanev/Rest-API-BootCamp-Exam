namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SoftUni_BootCamp.Data;
    using SoftUni_BootCamp.Data.Models;

    public class CandidateSkillsService : ICandidateSkillsService
    {
        private readonly ApplicationDbContext context;

        public CandidateSkillsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public CandidateSkill CreateCandidateSkill(string candidateId, Skill input)
        {
            var candidateSkill = new CandidateSkill
            {
                CandidateId = candidateId,
                SkillId = input.Id,
                Skill = input,
            };

            this.context.CandidateSkills.Add(candidateSkill);
            this.context.SaveChanges();

            return candidateSkill;
        }

        public IEnumerable<CandidateSkill> GetByCandidateId(string candidateId)
        {
            var candidateSkills = this.context.CandidateSkills.Where(x => x.CandidateId == candidateId).ToList();
            return candidateSkills;
        }
    }
}
