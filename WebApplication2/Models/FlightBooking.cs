using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class FlightBooking
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int FlightBookingId { get; set; }
        public int FlightId { get; set; }
        public string CustomerName { get; set; } //
        public string CustomerSurname { get; set; } //
        public string Address { get; set; }
        public string IdNumber { get; set; }//
        public string PhoneNumber { get; set; }
        public string DateBooked { get; set; } //
        public string BoardDateAndTime { get; set; } //
        public string TicketNumber { get; set; } //
        public virtual Flight Flight { get; set; } //
    }
}
