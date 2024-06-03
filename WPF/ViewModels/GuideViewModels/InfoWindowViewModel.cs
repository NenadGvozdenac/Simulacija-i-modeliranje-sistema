using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuideViews;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Diagnostics;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class InfoWindowViewModel
    {

        public EventHandler<EventArgs> quitEvent { get; set; }
        public InfoWindow infoWindow { get; set; }

        public User user { get; set; }

        public float avgGrade { get; set; }

        public int numberOfTours { get; set; }

        public string language { get; set; }

        public string name { get; set; }

        public string status { get; set; }

        public InfoWindowViewModel(InfoWindow _infoWindow, User _user) 
        { 
            infoWindow = _infoWindow;
            user = _user;
            Update();
        
        }

        public void Update()
        {
            GuideInfo guideInfo = GuideService.GetInstance().GetByGuideId(user.Id);
            avgGrade = guideInfo.AvrageGrade;
            numberOfTours = guideInfo.NumberOfToursThisYear;
            language = guideInfo.Language;
            name = user.Username;
            if(guideInfo.IsSuperGuide == true)
            {
                status = "superguide";
            }
            else
            {
                status = "guide";
            }
        }

        internal void quit_Click()
        {
            List<TourStartTime> dates = TourStartTimeService.GetInstance().GetAll();
            List<TouristReservation> reservations = TourReservationService.GetInstance().GetAll();
            foreach (TourStartTime date in dates)
            {
                Tour tour = TourService.GetInstance().GetById(date.TourId);
                if (tour.OwnerId == user.Id && date.Status == "scheduled")
                {
                    date.Status = "canceled";
                    TourStartTimeService.GetInstance().Update(date);

                    foreach(TouristReservation reservation in reservations)
                    {
                        if(reservation.Id_TourTime == date.Id)
                        {
                           TourVoucher voucher = new TourVoucher();
                            voucher.TouristId = reservation.Id_Tourist;
                            voucher.ExpirationDate = DateTime.Now.AddDays(365);
                            TourVoucherService.GetInstance().Add(voucher);

                        }
                    }
                }
                
            }
            GuideService.GetInstance().DeleteGuide(user.Id);
            quitEvent?.Invoke(this, EventArgs.Empty);
            infoWindow.Close();

        }

        internal void GeneratePdfReport_Click(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            List<TourStartTime> dates = new List<TourStartTime>();
            foreach (TourStartTime date in TourStartTimeService.GetInstance().GetAll()) 
            {
                if(date.Status == "scheduled" && TourService.GetInstance().GetById(date.TourId).OwnerId == user.Id && date.Time > startDate && date.Time < endDate)
                       dates.Add(date);
            }

            if (dates == null || dates.Count == 0)
            {
                MessageBox.Show("No tours found in the selected date range.");
                return;
            }

            GeneratePdf(dates);
        }


        private void GeneratePdf(List<TourStartTime> dates)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                try
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Add title
                    Font titleFont = FontFactory.GetFont("Arial", 36, Font.BOLD, BaseColor.DARK_GRAY);
                    iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Tours Report", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    title.SpacingAfter = 30f;
                    document.Add(title);

                    // Add date range
                    Font infoFont = FontFactory.GetFont("Arial", 18, Font.ITALIC, BaseColor.GRAY);
                    iTextSharp.text.Paragraph dateRange = new iTextSharp.text.Paragraph($"From {infoWindow.startPicker.SelectedDate.Value.ToShortDateString()} to {infoWindow.endPicker.SelectedDate.Value.ToShortDateString()}", infoFont);
                    dateRange.Alignment = Element.ALIGN_CENTER;
                    dateRange.SpacingAfter = 30f;
                    document.Add(dateRange);

                    // Create table for tour details
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    table.SpacingAfter = 20f;
                    table.SetWidths(new float[] { 3f, 2f });

                    // Add table header
                    Font headerFont = FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.WHITE);
                    PdfPCell headerCell1 = new PdfPCell(new Phrase("Tour Name", headerFont));
                    headerCell1.BackgroundColor = new BaseColor(79, 129, 189); // Blue
                    headerCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell headerCell2 = new PdfPCell(new Phrase("Tour Date", headerFont));
                    headerCell2.BackgroundColor = new BaseColor(79, 129, 189); // Blue
                    headerCell2.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.AddCell(headerCell1);
                    table.AddCell(headerCell2);

                    // Add tour details to table
                    Font cellFont = FontFactory.GetFont("Arial", 16, BaseColor.BLACK);
                    foreach (var date in dates)
                    {
                        PdfPCell cell1 = new PdfPCell(new Phrase(TourService.GetInstance().GetById(date.TourId).Name, cellFont));
                        cell1.Padding = 20f;
                        cell1.BackgroundColor = new BaseColor(210, 230, 255); // Light Blue
                        PdfPCell cell2 = new PdfPCell(new Phrase(date.Time.ToString("MMMM dd, yyyy"), cellFont));
                        cell2.Padding = 20f;
                        cell2.BackgroundColor = new BaseColor(210, 230, 255); // Light Blue

                        table.AddCell(cell1);
                        table.AddCell(cell2);
                    }

                    document.Add(table);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error generating PDF: {ex.Message}");
                    return;
                }
                finally
                {
                    document.Close();
                }

                // Convert the document to a byte array
                byte[] pdfBytes = stream.ToArray();

                // Offer the PDF as a download
                OfferPdfAsDownload(pdfBytes, "ToursReport.pdf");
            }
        }




        private void OfferPdfAsDownload(byte[] pdfBytes, string fileName)
        {
            // Prompt user to save the PDF file
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = fileName; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                File.WriteAllBytes(dlg.FileName, pdfBytes);
                MessageBox.Show("PDF file downloaded successfully.");
            }
        }

        private void AddTableCell(PdfPTable table, string label, string value, Font labelFont, Font valueFont)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(label, labelFont));
            labelCell.Border = PdfPCell.NO_BORDER;
            labelCell.PaddingBottom = 5f;
            labelCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell valueCell = new PdfPCell(new Phrase(value, valueFont));
            valueCell.Border = PdfPCell.NO_BORDER;
            valueCell.PaddingBottom = 5f;
            valueCell.HorizontalAlignment = Element.ALIGN_LEFT;

            table.AddCell(labelCell);
            table.AddCell(valueCell);
        }





    }
}
