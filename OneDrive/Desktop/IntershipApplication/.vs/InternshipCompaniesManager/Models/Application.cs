using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;


namespace InternshipCompaniesManager.Models
{
    public class Application
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime AppliedOn { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        public string PortfolioLink { get; set; }

        public string CVFilePath { get; set; }

        public string Q1 { get; set; } // zosto si motiviran
        public string Q2 { get; set; } // zosto ovaa kompanija



    }
}