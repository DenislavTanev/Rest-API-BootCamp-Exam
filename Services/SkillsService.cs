namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using SoftUni_BootCamp.Data;
    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public class SkillsService : ISkillsService
    {
        private readonly ApplicationDbContext context;

        public SkillsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateSkill(SkillInputModel input)
        {
            var skill = new Skill
            {
                Name = input.Name
            };

            this.context.Skills.Add(skill);
            this.context.SaveChanges();
        }

        public IEnumerable<Skill> GetAllSkills()
        {
            return this.context.Skills.ToList();
        }

        public Skill GetSkillById(string skillId)
        {
            var skill = this.context.Skills.FirstOrDefault(x => x.Id == skillId);
            return skill;
        }

        public Skill GetSkillByName(string name)
        {
            var skill = this.context.Skills.FirstOrDefault(x => x.Name == name);
            return skill;
        }

        public string GetSkillsWhichHaveCandidates()
        {
            var candidateSkills = this.context.CandidateSkills.ToList();
            var skills = this.context.Skills.ToList();
            var activeSkills = new HashSet<string>();

            foreach (var candidateSkill in candidateSkills)
            {
                var getSkills = skills.FirstOrDefault(x => x.Id == candidateSkill.SkillId);
                if (!activeSkills.Contains(getSkills.Name))
                {
                    activeSkills.Add(getSkills.Name);
                }
            }

            return string.Join(" ", activeSkills);
        }
    }
}
