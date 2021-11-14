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

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        public string DateFlight { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        public string DateReturn { get; set; }

        //public bool returnTicket { get; set; }

        [Display(Name = "Number of Adults")]
        public int NumA { get; set; }

        [Display(Name = "Number of Children")]
        public int NumC { get; set; }

        [Display(Name = "Number of Infants(Less than 1 Year old)")]
        public int NumI { get; set; }

        public double TotalCost { get; set; }

        public string CustomerName { get; set; } //
        public string CustomerSurname { get; set; } //
        public string Address { get; set; }
        public string IdNumber { get; set; }//
        public string PhoneNumber { get; set; }
        public string DateBooked { get; set; } //
        public string BoardDateAndTime { get; set; } //
        public string TicketNumber { get; set; } //     

    }
}
