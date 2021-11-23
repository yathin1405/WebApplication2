using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class checkIN
    {
        [Range(0, 50, ErrorMessage = "Please enter a number between 0 and 50.")]
        [Display(Name = "Number of Adults")]
        public int NumA { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a number between 0 and 50.")]
        [Display(Name = "Number of Children")]
        public int NumC { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a number between 0 and 50.")]
        [Display(Name = "Number of Infants(Less than 1 Year old)")]
        public int NumI { get; set; }
    }
}