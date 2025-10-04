using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InternshipCompaniesManager.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string University { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}