namespace SoftUni_BootCamp.Data.Models
{
    public class Interview
    {
        public string CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        public string JobId { get; set; }

        public Job Job { get; set; }
    }
}
