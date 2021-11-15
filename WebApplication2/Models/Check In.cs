using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Check_In
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Check_In_Id { get; set; }

        [Required]
        [Display(Name = "Please Enter Flight Reference")]
        public string fRef { get; set; }
    }
}