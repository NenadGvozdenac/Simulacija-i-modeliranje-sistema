using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BookingApp.WPF.DTOs.GuestDTOs;
using BookingApp.Application.UseCases;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace BookingApp.WPF.Views.GuestViews.Components;

public partial class UpcomingReservationsCard : UserControl
{
    public event EventHandler<int> RescheduleClicked;
    public event EventHandler<int> CancelClicked;
    public UpcomingReservationsCard()
    {
        InitializeComponent();
    }

    private void UpdateButtonState(object sender, DependencyPropertyChangedEventArgs e)
    {

        UpcomingReservationsDTO reservation = (UpcomingReservationsDTO)DataContext;
            
            if (reservation.RemainingDaysToCancel <= 0)
            {
                CancelReservationButton.IsEnabled = false;
                CancelReservationButton.Content = "Can't cancel the reservation";
                RemainingDays_TextBlock.Text = "Remaining days to cancel: 0";
            }
            else
            {
                CancelReservationButton.IsEnabled = true;
                CancelReservationButton.Content = "Tap here to cancel the reservation";
            }

    }
    private void Reschedule_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is UpcomingReservationsDTO reservation)
        {
            int reservationId = reservation.ReservationId;

            RescheduleClicked?.Invoke(this, reservationId);
        }
    }

    private void CancelReservationButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is UpcomingReservationsDTO reservation)
        {
            int reservationId = reservation.ReservationId;

            CancelClicked?.Invoke(this, reservationId);
        }
    }

    private void DownloadPdf_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is UpcomingReservationsDTO reservation)
        {
            // Generate PDF with reservation details
            byte[] pdfBytes = GeneratePdf(reservation);

            // Offer the PDF as a download
            OfferPdfAsDownload(pdfBytes, "Reservation_Details_" + AccommodationReservationService.GetInstance().GetById(reservation.ReservationId).FirstDateOfStaying.ToString("yyyy-MM-dd") + ".pdf");
        }
    }

    private byte[] GeneratePdf(UpcomingReservationsDTO reservation)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            AccommodationReservation accommodationReservation = AccommodationReservationService.GetInstance().GetById(reservation.AccommodationId);
            Accommodation accommodation = AccommodationService.GetInstance().GetById(accommodationReservation.AccommodationId);

            // Set up fonts
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, BaseColor.BLACK);
            Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.DARK_GRAY);
            Font valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK);
            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.DARK_GRAY);
            Font thankYouFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLDOBLIQUE, 16);

            // Title
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("Reservation Details", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20f;
            document.Add(title);

            // Create a table for reservation details
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1f, 2f });
            table.SpacingAfter = 20f;

            // Add reservation details
            AddReservationDetail(table, "Reservation ID", $"#{reservation.ReservationId}", labelFont, valueFont);
            AddReservationDetail(table, "Guest Name", UserService.GetInstance().GetById(accommodationReservation.UserId).Username, labelFont, valueFont);
            AddReservationDetail(table, "Accommodation", accommodation.Name, labelFont, valueFont);
            AddReservationDetail(table, "Location", $"{accommodation.Location.City}, {accommodation.Location.Country}", labelFont, valueFont);
            AddReservationDetail(table, "Price", $"{accommodation.Price}$", labelFont, valueFont);
            AddReservationDetail(table, "Stay Dates", $"{accommodationReservation.FirstDateOfStaying.ToString("MMMM dd, yyyy")} - {accommodationReservation.LastDateOfStaying.ToString("MMMM dd, yyyy")}", labelFont, valueFont);
            AddReservationDetail(table, "Number of Guests", $"{accommodationReservation.GuestsNumber}", labelFont, valueFont);
            AddReservationDetail(table, "Accommodation Type", $"{accommodation.Type}", labelFont, valueFont);

            // Add the table to the document
            document.Add(table);

            // Cancellation Policy
            iTextSharp.text.Paragraph cancellationPolicyTitle = new iTextSharp.text.Paragraph("Cancellation Policy:", labelFont);
            cancellationPolicyTitle.SpacingBefore = 20f;
            document.Add(cancellationPolicyTitle);

            iTextSharp.text.Paragraph cancellationPolicyText = new iTextSharp.text.Paragraph();
            cancellationPolicyText.Add(new Chunk("Guests may cancel their reservation up to 24 hours prior to the start date of their stay, unless otherwise specified by the property owner. If the property owner has set additional cancellation restrictions (e.g., requiring cancellation at least 3 days in advance), those restrictions must be adhered to.\n\n", infoFont));
            cancellationPolicyText.Add(new Chunk("Upon cancellation of a reservation, notification will be sent to the property owner, and the accommodation will become available for booking on the canceled dates.", infoFont));
            document.Add(cancellationPolicyText);

            //newline
            document.Add(new Chunk("\n"));

            // Additional Information
            iTextSharp.text.Paragraph info = new iTextSharp.text.Paragraph($"Please feel free to contact us if you have any questions or need assistance with your reservation.", infoFont);
            info.Alignment = Element.ALIGN_CENTER;
            info.SpacingAfter = 20f;
            document.Add(info);

            // Thank You Message
            iTextSharp.text.Paragraph thankYou = new iTextSharp.text.Paragraph("Thank you for choosing our accommodation! We hope you will have a fantastic stay!", thankYouFont);
            thankYou.Alignment = Element.ALIGN_CENTER;
            thankYou.SpacingAfter = 20f;
            document.Add(thankYou);

            document.Close();

            return stream.ToArray();
            }
        }

    private void AddReservationDetail(PdfPTable table, string label, string value, Font labelFont, Font valueFont)
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
}
