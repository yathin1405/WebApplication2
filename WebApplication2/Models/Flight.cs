using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Flight
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int FlightId { get; set; }

        [Display(Name = "Flight Types")]
        public FlightList FlightL { get; set; }
        public enum FlightList
        {
            Kulula,  //Cheapest
            BritishAirways,   //Most
            SAA      //Meh
        }

        [Display(Name = "From")]
        public FromList FromL { get; set; }
        public enum FromList
        {
            Durban,
            Johannesburg,
            CapeTown
        }

        public DestinationList DestinationL { get; set; }
        public enum DestinationList
        {
            Durban,
            Johannesburg,
            CapeTown
        }
        public ClassList ClassL { get; set; }
        public enum ClassList
        {
            Economy,
            Business,
            First
        }

        [DataType(DataType.Date, ErrorMessage = "Back date entry not allowed")]

        public string DateFlight { get; set; }

        [DataType(DataType.Date,ErrorMessage = "Back date entry not allowed")]
        public string DateReturn { get; set; }

        public bool returnTicket { get; set; }
        public string Email { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a number between 0 and 50.")]
        [Display(Name = "Number of Adults")]
        public int NumA { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a number between 0 and 50.")]
        [Display(Name = "Number of Children")]
        public int NumC { get; set; }
        [Range(0, 50, ErrorMessage = "Please enter a number between 0 and 50.")]
        [Display(Name = "Number of Infants(Less than 1 Year old)")]
        public int NumI { get; set; }
        [DataType(DataType.Time, ErrorMessage = "Time only")]

        public string DepartureTime { get; set; }

        [DataType(DataType.Time, ErrorMessage = "Time only")]

        public string Return_Time { get; set; }
        public double TotalCost { get; set; }

        public string CustomerName { get; set; } //
        public string CustomerSurname { get; set; } //
        public string Address { get; set; }
        public string IdNumber { get; set; }//
        public string PhoneNumber { get; set; }
        public string DateBooked { get; set; } //
        public string BoardDateAndTime { get; set; } //
        public string TicketNumber { get; set; } //
        public string RefID { get; set; }

        public string determineKey()
        {
            Random ran = new Random();
            string r = "";



            string firstTwo = CustomerName.Substring(0, 2);
            string nextTwo = IdNumber.Substring(4, 3);
            int randomOne = ran.Next(1, 101);
            int randomTwo = ran.Next(101, 201);
            int diff = (randomTwo - randomOne);
            string d = Convert.ToString(diff);
            r = firstTwo + nextTwo + d + "";
            return r + "";
        }
        public class StartDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime _dateStart = Convert.ToDateTime(value);
                if (_dateStart >= DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

        }
    }
}
