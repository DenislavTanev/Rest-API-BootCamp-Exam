namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public interface ISkillsService
    {
        void CreateSkill(SkillInputModel input);

        string GetSkillsWhichHaveCandidates();

        Skill GetSkillById(string skillId);

        Skill GetSkillByName(string name);

        IEnumerable<Skill> GetAllSkills();
    }
}
