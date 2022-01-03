namespace SoftUni_BootCamp.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using SoftUni_BootCamp.Data.Models;
    using SoftUni_BootCamp.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewsService interviewsService;

        public InterviewsController(IInterviewsService interviewsService)
        {
            this.interviewsService = interviewsService;
        }

        [HttpGet]
        public IEnumerable<Interview> Get()
        {
            var result = this.interviewsService.GetAllInterviews();
            return result;
        }
    }
}
