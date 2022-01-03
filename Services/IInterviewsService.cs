namespace SoftUni_BootCamp.Services
{
    using SoftUni_BootCamp.Data.Models;
    using System.Collections.Generic;

    public interface IInterviewsService
    {
        void CreateAvailableInterviews(Dictionary<string, string> suitableCandidates);

        List<KeyValuePair<string, string>> CheckSuitableCandidates();

        IEnumerable<Interview> GetAllInterviews();
    }
}
