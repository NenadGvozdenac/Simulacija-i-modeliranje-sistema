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
    /// Interaction logic for ComplexRequestPartCard.xaml
    /// </summary>

    
    public partial class ComplexRequestPartCard : UserControl
    {
        public EventHandler<BeginButtonClickedEventArgs> AcceptButtonClicked_Event { get; set; }
        public ComplexRequestPartCard()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            
            OnAcceptButtonClicked(new BeginButtonClickedEventArgs(Convert.ToInt32(RequestId_TextBlock.Text), Convert.ToDateTime(DateTime.Now)));
            
        }

        private void OnAcceptButtonClicked(BeginButtonClickedEventArgs e)
        {
            AcceptButtonClicked_Event?.Invoke(this, e);
        }



    }
}
