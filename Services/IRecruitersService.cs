namespace SoftUni_BootCamp.Services
{
    using System.Collections.Generic;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Models.InputModels;

    public interface IRecruitersService
    {
        Recruiter CreateRecruiter(RecruiterInputModel input);

        IEnumerable<Recruiter> GetAllRecruites();

        IEnumerable<Recruiter> GetRecruitersByLevel(int level);

        Recruiter GetRecruiterByName(string lastName);
    }
}
