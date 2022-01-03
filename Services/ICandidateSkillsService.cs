namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;

    using SoftUni_BootCamp.Data.Models;

    public interface ICandidateSkillsService
    {
        CandidateSkill CreateCandidateSkill(string candidateId, Skill input);

        IEnumerable<CandidateSkill> GetByCandidateId(string candidateId);
    }
}
