using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Net.Mail;
using System.IO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using System.Text;
using System;
using System.Data.Entity;



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

        public async Task<ActionResult> checkIN(int id)
        {
            double insurance = 0;
            Flight flight = await db.Flights.FindAsync(id);
            insurance = flight.NumA * 1000;
            flight.InsName = insurance;
            await db.SaveChangesAsync();
            return RedirectToAction("Details" + "/" + id);
        }
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
          

            PdfDocument document = new PdfDocument();
            //Adds page settings
            document.PageSettings.Orientation = PdfPageOrientation.Portrait;
            document.PageSettings.Margins.All = 50;
            //Adds a page to the document
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            //Loads the image from disk
            //PdfImage image = PdfImage.FromStream(msS);
            RectangleF bounds = new RectangleF(10, 10, 200, 200);
            //Draws the image to the PDF page
            //page.Graphics.DrawImage(image, bounds);
            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            bounds = new RectangleF(0, bounds.Bottom + 90, graphics.ClientSize.Width, 30);
            //Draws a rectangle to place the heading in that region.
            graphics.DrawRectangle(solidBrush, bounds);
            //Creates a font for adding the heading in the page
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 13);
            //Creates a text element to add the invoice number
            PdfTextElement element = new PdfTextElement("Thank you! You have successfully booked the " + flight.FlightId + " Flight!", subHeadingFont);
            element.Brush = PdfBrushes.White;
            //Draws the heading on the page
            PdfLayoutResult res = element.Draw(page, new PointF(10, bounds.Top + 8));
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            //Creates text elements to add the address and draw it to the page.
            element = new PdfTextElement("This ticket belongs to: " + FirstName, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(16, 36, 7));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 45));
            element = new PdfTextElement("Event Location: " + flight.FlightL + ".", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));
            element = new PdfTextElement("Description: " + flight.FromL + ".", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));
            element = new PdfTextElement("Price: R" + flight.TotalCost, timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(80, 138, 4));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));

            PdfPen linePen = new PdfPen(new PdfColor(80, 138, 4), 0.70f);
            PointF startPoint = new PointF(0, res.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, res.Bounds.Bottom + 5);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);

            PdfFont notTimesRoman = new PdfStandardFont(PdfFontFamily.Courier, 16);
            element = new PdfTextElement("Your Ticket Number is" + flight.TicketNumber, notTimesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(48, 5, 5));
            res = element.Draw(page, new PointF(10, res.Bounds.Bottom + 15));

            linePen = new PdfPen(new PdfColor(80, 138, 4), 0.70f);
            startPoint = new PointF(0, res.Bounds.Bottom + 3);
            endPoint = new PointF(graphics.ClientSize.Width, res.Bounds.Bottom + 5);
            //Draws a line at the bottom of the address
            graphics.DrawLine(linePen, startPoint, endPoint);


            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream);
            outputStream.Position = 0;

            var invoicePdf = new System.Net.Mail.Attachment(outputStream, System.Net.Mime.MediaTypeNames.Application.Pdf);
            string docname = "Invoice.pdf";
            invoicePdf.ContentDisposition.FileName = docname;

            MailMessage mail = new MailMessage();
            string emailTo = User.Identity.Name;
            MailAddress from = new MailAddress("21642835@dut4life.ac.za");
            mail.From = from;
            mail.Subject = "Your e-Ticket for tours: " + flight.FlightL;
            mail.Body = "Dear " + FirstName + ", find your invoice in the attached PDF document.";
            mail.To.Add(emailTo);
            mail.Attachments.Add(invoicePdf);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential("21642835@dut4life.ac.za", "$$Dut980514");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = networkCredential;
            smtp.Port = 587;
            smtp.Send(mail);
            //Clean-up.
            //Close the document.
            document.Close(true);
            //Dispose of email.
            mail.Dispose();




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
