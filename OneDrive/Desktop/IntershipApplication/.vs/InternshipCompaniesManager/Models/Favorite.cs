using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InternshipCompaniesManager.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}