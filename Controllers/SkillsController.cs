namespace SoftUni_BootCamp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Services;

    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsService skillsService;

        public SkillsController(ISkillsService skillsService)
        {
            this.skillsService = skillsService;
        }

        [HttpGet("{id}")]
        public Skill Get(string id)
        {
            var skill = this.skillsService.GetSkillById(id);
            return skill;
        }

        [Route("/Name")]
        [HttpGet]
        public ActionResult<Skill> Name(string name)
        {
            var skill = this.skillsService.GetSkillByName(name);
            return skill;
        }

        [Route("/All")]
        [HttpGet]
        public ActionResult<IEnumerable<Skill>> All()
        {
            var skill = this.skillsService.GetAllSkills().ToList();
            return skill;
        }

        [Route("/Active")]
        [HttpGet]
        public string Get()
        {
            var result = this.skillsService.GetSkillsWhichHaveCandidates();

            return result;
        }
    }
}
