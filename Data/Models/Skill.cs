namespace SoftUni_BootCamp.Data.Models
{
    using System;

    public class Skill
    {
        public Skill()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
