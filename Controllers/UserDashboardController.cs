using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Models;
using System.IO;
using System.Linq;

// PDF alias
using PdfDocument = iTextSharp.text.Document;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PaymentSystem.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly AppDbContext _context;

        public UserDashboardController(AppDbContext context)
        {
            _context = context;
        }

        // ================= USER DASHBOARD =================
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Auth");

            var user = _context.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var payments = _context.Payments
                .Where(p => p.UserId == user.Id)
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            var model = new UserDashboardViewModel
            {
                User = user,
                Payments = payments
            };

            return View(model);
        }

        // ================= DOWNLOAD STATEMENT =================
        public IActionResult DownloadStatement()
        {
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Auth");

            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
                return RedirectToAction("Login", "Auth");

            var payments = _context.Payments
                .Where(p => p.UserId == user.Id)
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfDocument doc = new PdfDocument(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(doc, ms);

                doc.Open();

                // ================= COLORS =================
                BaseColor white = new BaseColor(255, 255, 255);
                BaseColor blue = new BaseColor(13, 110, 253);
                BaseColor lightGray = new BaseColor(245, 245, 245);

                // ================= LOGO =================
                string logoPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/image/image_Logo.jpeg"
                );

                if (System.IO.File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(80f, 80f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }

                // ================= TITLE =================
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph title = new Paragraph(
                    "GLOBALTRUST BANK\nTRANSACTION STATEMENT\n\n",
                    titleFont
                );
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                // ================= USER INFO =================
                PdfPTable infoTable = new PdfPTable(1);
                infoTable.WidthPercentage = 100;

                PdfPCell infoCell = new PdfPCell(new Phrase(
                    $"Customer: {user.Name}\n" +
                    $"Email: {user.Email}\n" +
                    $"Generated: {DateTime.Now:dd MMM yyyy HH:mm}"
                ));

                infoCell.Padding = 10;
                infoCell.BackgroundColor = lightGray;
                infoTable.AddCell(infoCell);

                doc.Add(infoTable);
                doc.Add(new Paragraph("\n"));

                // ================= TABLE =================
                PdfPTable table = new PdfPTable(5);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 25, 25, 15, 15, 20 });

                Font headerFont = FontFactory.GetFont(
                    FontFactory.HELVETICA_BOLD, 10, white);

                string[] headers = { "Recipient", "Bank", "Amount", "Currency", "Date" };

                foreach (var h in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(h, headerFont));
                    cell.BackgroundColor = blue;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 6;
                    table.AddCell(cell);
                }

                // ================= ROWS =================
                bool isAlt = false;

                foreach (var p in payments)
                {
                    // ✅ FIXED HERE
                    BaseColor rowColor = isAlt ? lightGray : white;

                    PdfPCell c1 = new PdfPCell(new Phrase(p.RecipientName));
                    PdfPCell c2 = new PdfPCell(new Phrase(p.BankName));
                    PdfPCell c3 = new PdfPCell(new Phrase("R " + p.Amount.ToString("0.00")));
                    PdfPCell c4 = new PdfPCell(new Phrase(p.Currency));
                    PdfPCell c5 = new PdfPCell(new Phrase(p.DateCreated.ToString("dd MMM yyyy")));

                    c1.BackgroundColor = rowColor;
                    c2.BackgroundColor = rowColor;
                    c3.BackgroundColor = rowColor;
                    c4.BackgroundColor = rowColor;
                    c5.BackgroundColor = rowColor;

                    table.AddCell(c1);
                    table.AddCell(c2);
                    table.AddCell(c3);
                    table.AddCell(c4);
                    table.AddCell(c5);

                    isAlt = !isAlt;
                }

                doc.Add(table);

                // ================= FOOTER =================
                doc.Add(new Paragraph("\n\n"));

                Paragraph footer = new Paragraph(
                    "This is a system-generated statement from GlobalTrust Digital Bank.",
                    FontFactory.GetFont(FontFactory.HELVETICA, 9)
                );

                footer.Alignment = Element.ALIGN_CENTER;
                doc.Add(footer);

                doc.Close();

                return File(ms.ToArray(),
                    "application/pdf",
                    "Transaction_Statement.pdf");
            }
        }
    }
}