using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Survey
    {

        public int SurveyId { get; set; }
        [Required(ErrorMessage = "Choose a valid Park")]
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        [Required(ErrorMessage = "Enter a valid email")]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Choose a valid State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Enter your activity level")]
        public string ActivityLevel { get; set; }
        public int VoteCount { get; set; }


    }
}
