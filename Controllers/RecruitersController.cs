namespace SoftUni_BootCamp.Controllers
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Services;

    [ApiController]
    [Route("[controller]")]
    public class RecruitersController : ControllerBase
    {
        private readonly IRecruitersService recruitersService;

        public RecruitersController(IRecruitersService recruitersService)
        {
            this.recruitersService = recruitersService;
        }

        [HttpGet]
        public IEnumerable<Recruiter> Get()
        {
            var allRecruiters = this.recruitersService.GetAllRecruites();
            return allRecruiters;
        }

        [HttpGet]
        public IEnumerable<Recruiter> Get(int level)
        {
            if (level == 0)
            {
                throw new ArgumentException("You need to enter level");
            }
            var rectuiter = this.recruitersService.GetRecruitersByLevel(level);
            return rectuiter;
        }
    }
}
