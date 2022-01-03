namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public interface ICandidatesService
    {
        bool IsFirstNameAvailable(string firstName);

        bool IsLastNameAvailable(string lastName);

        bool IsEmailAvailable(string email);

        IEnumerable<Candidate> GetAllCandidates();

        Candidate GetCandidateById(string candidateId);

        Recruiter GetCandidateRecruiterById(string candidateRecruiterId);

        Candidate CreateCandidate(CandidateInputModel input);

        Candidate UpdateCandidate(string id, CandidateInputModel input);

        bool DeleteCandidateById(string candidateId);
    }
}
