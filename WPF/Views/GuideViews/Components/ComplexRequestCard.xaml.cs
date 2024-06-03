using BookingApp.Application.UseCases;
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

namespace BookingApp.WPF.Views.GuideViews.Components
{
    /// <summary>
    /// Interaction logic for ComplexRequestCard.xaml
    /// </summary>
    public partial class ComplexRequestCard : UserControl
    {
        public UserControl user {  get; set; }

        public ComplexRequestCard()
        {
            InitializeComponent();
        }

        private void moreInfoClick(object sender, RoutedEventArgs e)
        {
            User user = UserService.GetInstance().GetById(Convert.ToInt32(guideId.Text));
            ComplexTourPartsWindow complexTourPartsWindow = new ComplexTourPartsWindow(Convert.ToInt32(idBox.Text), user);
            complexTourPartsWindow.complexTourPartsWindowViewModel.disableView_Handler += (s, e) => Accept_SomeEventHandler(s, e);
            complexTourPartsWindow.Show();
        }

        private void Accept_SomeEventHandler(object s, EventArgs e)
        {
           seeButton.IsEnabled = false;
           seeButton.Opacity = 0.3;
        }
 
    
    
    }
}
