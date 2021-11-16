using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Text;



namespace WebApplication2.Controllers
{
    [Authorize]
    public class Flights1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in db.Flights

                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.RefID.Contains(searchString)
                                       || s.RefID.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.CustomerSurname);
                    break;
                case "Referance":
                    students = students.OrderByDescending(s => s.RefID);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.DateBooked);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.DateBooked);
                    break;
                default:
                    students = students.OrderBy(s => s.CustomerSurname);
                    break;
            }

            return View(students.ToList());
        }

        // GET: Flights/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = await db.Flights.FindAsync(id);
            if (flight.DestinationL.ToString() == "Durban")
            {
                ViewBag.Destination = "Virginia Airport Hanger No.7, Fairway, Durban North, Durban, 4051";
            }
            else if (flight.DestinationL.ToString() == "Johannesburg")
            {
                ViewBag.Destination = "Airport Rd, Lanseria, 1748";
            }
            else if (flight.DestinationL.ToString() == "Cape Town")
            {
                ViewBag.Destination = "Matroosfontein, Cape Town, 7490";
            }
            else
            {
                ViewBag.Destination = "Virginia Airport Hanger No.7, Fairway, Durban North, Durban, 4051";
            }
            if (flight == null)
            {
                return HttpNotFound();
            }




            return View(flight);
        }
        //public ActionResult Detail()
        //{
        //    string id = User.Identity.Name;
        //    var Db = new ApplicationDbContext();
        //    var user = Db.Users.First(u => u.Email == id);
        //    var model = new RegisterViewModel(user);
        //    model.Flights.Sort();
        //    return View(model);
        //}
        //[HttpGet]
        //[Authorize]
        //public ActionResult guestDetail()
        //{
        //    List<Flight> gd = new List<Flight>();
        //    var list = (from u in db.Users.Where(x => x.Email == HttpContext.User.Identity.Name)
        //                join r in db.Flights on u.Email equals r.Email
        //                where u.Email == r.Email
        //                join rm in db.Rooms on r.RoomNo equals rm.RoomNo
        //                join rt in db.roomTypes on rm.RoomTypeId equals rt.RoomTypeId
        //                select new
        //                {
        //                    u.FirstName,
        //                    u.LastName,
        //                    u.Email,
        //                    u.PhoneNumber,
        //                    u.IdNumber,
        //                    r.CustomerName,
        //                    r.CustomerSurname,
        //                    r.DateBooked,
        //                    r.DateFlight,
        //                    r.DateReturn,
        //                    r.DestinationL,
        //                    r.FlightId,
        //                    r.FlightL,
        //                    r.FromL,
        //                    r.NumA,
        //                    r.NumC,
        //                    r.NumI,
        //                    r.RefID,
        //                    r.TotalCost,



        //                }).ToList();
        //    foreach (var it in list)
        //    {
        //        Flight gdd = new Flight();
        //        gdd.CustomerName = it.FirstName;
        //        gdd.CustomerSurname = it.LastName;
        //        gdd.Email = it.Email;
        //        gdd.DateFlight = it.DateFlight;
        //        gdd.DateReturn = it.DateReturn;
        //        gdd.DestinationL = it.DestinationL;
        //        gdd.FlightId = it.FlightId;
        //        gdd.FlightL = it.FlightL;
        //        gdd.FromL = it.FromL;
        //        gdd.IdNumber = it.IdNumber;
        //        gdd.NumA = it.NumA;
        //        gdd.NumC = it.NumC;
        //        gdd.NumI = it.NumI;
        //        gdd.RefID = it.RefID;

        //        gdd.BasicCharge = it.BasicCharge;
        //        gdd.TotalCost = it.TotalCost;
        //        gdd.Discount = it.Discount;

        //        gd.Add(gdd);
        //    }
        //    return View(gd);
        //}
        //public ActionResult ConfirmFlight(int? FlightID)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Flights.Add(flight);
        //        db.SaveChanges();
        //        Session["bookID"] = flight.FlightID;
        //        return RedirectToAction("Payment");
        //    }
        //    {
        //        if (FlightID == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        Flight reservation = db.Flights.Find(FlightID);
        //        if (reservation == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        Flight flight = db.Flights.Where(x => x.FlightID == flight.Email).FirstOrDefault();

        //        GuestDetails Gdetails = new GuestDetails()
        //        {
        //            checkInDate = reservation.checkInDate,
        //            checkOutDate = reservation.checkOutDate,
        //            numGuests = reservation.numGuests,
        //            DayNo = reservation.DayNo,
        //            bookings = 0,
        //            RoomNo = reservation.RoomNo,
        //            FloorNum = rooms.FloorNum,
        //            Type = rooms.Roomtype.Type,
        //            BasicCharge = rooms.Roomtype.BasicCharge,
        //            TotalPrice = reservation.TotalPrice
        //        };

        //        var rm = db.Flights.Find(reservation.FlightId);
        //        var rmb = db.roomTypes.Find(rm.RoomTypeId);
        //        gdd.CustomerName = it.FirstName;
        //        gdd.CustomerSurname = it.LastName;
        //        gdd.Email = it.Email;
        //        gdd.DateFlight = it.DateFlight;
        //        gdd.DateReturn = it.DateReturn;
        //        gdd.DestinationL = it.DestinationL;
        //        gdd.FlightId = it.FlightId;
        //        gdd.FlightL = it.FlightL;
        //        gdd.FromL = it.FromL;
        //        gdd.IdNumber = it.IdNumber;
        //        gdd.NumA = it.NumA;
        //        gdd.NumC = it.NumC;
        //        gdd.NumI = it.NumI;
        //        gdd.RefID = it.RefID;

        //        Flight Gdetails = new Flight();
        //        Gdetails.CustomerName = reservation.CustomerName;
        //        Gdetails.CustomerSurname = reservation.CustomerSurname;
        //        Gdetails.DateFlight = reservation.DateFlight;
        //        Gdetails.DateReturn = reservation.DateReturn; ;
        //        Gdetails.DestinationL = reservation.DestinationL;
        //        Gdetails.FlightId = reservation.FlightId;
        //        Gdetails.FlightL = reservation.FlightL;
        //        Gdetails.FromL = reservation.FromL;
        //        Gdetails.IdNumber = reservation.IdNumber;
        //        Gdetails.NumA = reservation.NumA;
        //        Gdetails.NumC = reservation.NumC;
        //        Gdetails.NumI = reservation.NumI;
        //        Gdetails.RefID = reservation.RefID;
        //        Gdetails.Email = reservation.Email;


        //        Gdetails.bookings = 0;
        //        Gdetails.TotalCost = rm.TotalCost;
        //        Gdetails.ReturnTicket_Price = rm.ReturnTicketPrice();
        //        Gdetails.Seat_Type_Calc = rm.SeatTypeCalc();
        //        Gdetails. = rm.BasicCharge;
        //        Gdetails.TotalPrice = reservation.TotalPrice;
        //        Gdetails.Discount = reservation.Discount;
        //        return View(Gdetails);
        //    }


        //}
        // GET: Flights/Create
        public ActionResult Create(string id)
        {

            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FlightId,FlightL,Email,Return_Time,DepartureTime,RefID,NumA,NumC,NumI,ClassL,FromL,DestinationL,DateFlight,DateReturn,returnTicket,TotalCost,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,BoardDateAndTime,TicketNumber")] Flight flight)
        {
            Flight flights = new Flight();

            double finalCost = 0;

            var vFirstName = User.Identity.GetFirstName();
            var vLastName = User.Identity.GetLastName();
            var vAddress = User.Identity.GetAddress();
            var vIdNumber = User.Identity.GetIDNumber();
            string FirstName = vFirstName.ToString();
            string LastName = vLastName.ToString();
            string IdNumber = vIdNumber.ToString();
            string Address = vAddress.ToString();

            flights.CustomerName = FirstName;
            flights.CustomerSurname = LastName;
            flights.IdNumber = IdNumber;
            flights.Address = Address;
            flights.DateBooked = DateTime.Now.ToString();

            //DateTime returns = DateTime.Parse(flight.DateReturn);
            DateTime departs = DateTime.Parse(flight.DateFlight);
            DateTime departsTime = DateTime.Parse(flight.DepartureTime);

            //TimeSpan DaysBooked = returns.Subtract(departs);
            //int numDays = DaysBooked.Days;

            flights.DateFlight = departs.ToString();
            flights.DepartureTime = departsTime.ToString();
            //flights.DateReturn = returns.ToString();

            string Ticket = "#300" + flights.FlightId + FirstName.Substring(0, 1) + LastName.Substring(0, 1) + DateTime.Now.ToString("FFFFF"); //100000ths of a second makes the ticket unique. (FFFFF)

            flights.TicketNumber = Ticket;

            //Destinations

            if (flight.DestinationL.ToString() == "Durban" && flight.FromL.ToString() == "Johannesburg" && flight.FlightL.ToString() == "BritishAirways")
            {

                finalCost += ((650 * flight.NumA) + ((650 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Durban" && flight.FromL.ToString() == "Johannesburg" && flight.FlightL.ToString() == "SAA")
            {
                finalCost += ((600 * flight.NumA) + ((600 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Durban" && flight.FromL.ToString() == "Johannesburg" && flight.FlightL.ToString() == "Kulula")
            {
                finalCost += ((550 * flight.NumA) + ((550 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Durban" && flight.FromL.ToString() == "CapeTown" && flight.FlightL.ToString() == "BritishAirways")
            {
                finalCost += ((800 * flight.NumA) + ((800 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Durban" && flight.FromL.ToString() == "CapeTown" && flight.FlightL.ToString() == "SAA")
            {
                finalCost += ((850 * flight.NumA) + ((850 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Durban" && flight.FromL.ToString() == "CapeTown" && flight.FlightL.ToString() == "Kulula")
            {
                finalCost += ((900 * flight.NumA) + ((900 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Johannesburg" && flight.FromL.ToString() == "Durban" && flight.FlightL.ToString() == "BritishAirways")
            {
                finalCost += ((650 * flight.NumA) + ((650 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Johannesburg" && flight.FromL.ToString() == "Durban" && flight.FlightL.ToString() == "SAA")
            {
                finalCost += ((600 * flight.NumA) + ((600 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Johannesburg" && flight.FromL.ToString() == "Durban" && flight.FlightL.ToString() == "Kulula")
            {
                finalCost += ((700 * flight.NumA) + ((700 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Johannesburg" && flight.FromL.ToString() == "CapeTown" && flight.FlightL.ToString() == "BritishAirways")
            {
                finalCost += ((750 * flight.NumA) + ((750 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Johannesburg" && flight.FromL.ToString() == "CapeTown" && flight.FlightL.ToString() == "SAA")
            {
                finalCost += ((1150 * flight.NumA) + ((1150 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "Johannesburg" && flight.FromL.ToString() == "CapeTown" && flight.FlightL.ToString() == "Kulula")
            {
                finalCost += ((1200 * flight.NumA) + ((1200 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "CapeTown" && flight.FromL.ToString() == "Durban" && flight.FlightL.ToString() == "BritishAirways")
            {
                finalCost += ((1250 * flight.NumA) + ((1250 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "CapeTown" && flight.FromL.ToString() == "Durban" && flight.FlightL.ToString() == "SAA")
            {
                finalCost += ((1300 * flight.NumA) + ((1300 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "CapeTown" && flight.FromL.ToString() == "Durban" && flight.FlightL.ToString() == "Kulula")
            {
                finalCost += ((1100 * flight.NumA) + ((1100 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "CapeTown" && flight.FromL.ToString() == "Johannesburg" && flight.FlightL.ToString() == "BritishAirways")
            {
                finalCost += ((1000 * flight.NumA) + ((1000 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "CapeTown" && flight.FromL.ToString() == "Johannesburg" && flight.FlightL.ToString() == "SAA")
            {
                finalCost += ((1350 * flight.NumA) + ((1350 * 0.5) * flight.NumC));
            }
            else if (flight.DestinationL.ToString() == "CapeTown" && flight.FromL.ToString() == "Johannesburg" && flight.FlightL.ToString() == "Kulula")
            {
                finalCost += finalCost += ((1400 * flight.NumA) + ((1400 * 0.5) * flight.NumC));
            }


            //Class
            if (flight.ClassL.ToString() == "First")
            {
                finalCost += 150 * (flight.NumA + flight.NumC);
            }
            else if (flight.ClassL.ToString() == "Business")
            {
                finalCost += 100 * (flight.NumA + flight.NumC);
            }
            else if (flight.ClassL.ToString() == "Economy")
            {
                finalCost += 50 * (flight.NumA + flight.NumC);
            }

            ////Aircrafts
            //if (flight.FlightL.ToString() == "Kulula")
            //{
            //    finalCost += 600;
            //}
            //else if (flight.FlightL.ToString() == "BritishAirways")
            //{
            //    finalCost += 800;
            //}
            //else if (flight.FlightL.ToString() == "SAA")
            //{
            //    finalCost += 450;
            //}

            //finalCost = finalCost * 2;

            //if (flight.returnTicket == true)
            //{
            //    finalCost = finalCost * 2;
            //}
            flights.RefID = flights.determineKey();
            flights.TotalCost = finalCost;
            flights.FlightL = flight.FlightL;
            flights.DestinationL = flight.DestinationL;
            flights.FromL = flight.FromL;
            flights.Return_Time = flight.Return_Time;
            flights.DepartureTime = flight.DepartureTime;
            flights.NumA = flight.NumA;
            flights.NumC = flight.NumC;
            flights.NumI = flight.NumI;
            db.Flights.Add(flights);
            await db.SaveChangesAsync();







            try
            {
                // Retrieve required values for the PayFast Merchant
                string name = "ParadiseTravels Flight: " + " " + flight.DestinationL + " "
                    + " " + "with" + flight.FlightL
                    + " " + "Number of Adults" + " " + flight.NumA
                    + " " + "Number of Children" + " " + flight.NumC
                    + " " + "Number of Infants" + " " + flight.NumI;
                string description = "This is a once-off and non-refundable payment. ";

                string site = "https://sandbox.payfast.co.za/eng/process";
                string merchant_id = "";
                string merchant_key = "";

                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "	10024488";
                    merchant_key = "gu9usv3kj7hng";
                }

                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(flights.FlightId.ToString()));
                str.Append("&amount=" + HttpUtility.UrlEncode(flights.TotalCost.ToString()));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                return Redirect(site + str.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ActionResult> Board(int id)
        {
            Flight flight = await db.Flights.FindAsync(id);
            flight.BoardDateAndTime = DateTime.Now.ToString();
            await db.SaveChangesAsync();
            return RedirectToAction("Details" + "/" + id);
        }


        // GET: Flights/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = await db.Flights.FindAsync(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FlightId,FlightL,DestinationL,DateFlight,DateReturn,returnTicket,TotalCost,CustomerName,CustomerSurname,Address,IdNumber,PhoneNumber,DateBooked,BoardDateAndTime,TicketNumber")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = await db.Flights.FindAsync(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Flight flight = await db.Flights.FindAsync(id);
            db.Flights.Remove(flight);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
