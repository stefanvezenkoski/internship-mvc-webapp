using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace InternshipCompaniesManager.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Position { get; set; }

        [Display(Name = "Sector / Industry")]
        public string Sector { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string ContactPhone { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public int OpenPositions { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

    }
}