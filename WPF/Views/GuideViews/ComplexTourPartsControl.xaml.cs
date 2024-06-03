using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.GuideViewModels;
using BookingApp.WPF.Views.GuideViews.Components;
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

namespace BookingApp.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for ComplexTourPartsControl.xaml
    /// </summary>
    public partial class ComplexTourPartsControl : UserControl
    {
        public ComplexTourPartsControlViewModel complexTourPartsControlViewModel { get; set; }

        public EventHandler<BeginButtonClickedEventArgs> AcceptButtonClickedControl { get; set; }

        public ComplexTourPartsControl(int id)
        {
            InitializeComponent();
            complexTourPartsControlViewModel = new ComplexTourPartsControlViewModel(this, id);
            
            DataContext = complexTourPartsControlViewModel;

        }

        public void ComplexCard_BeginButtonClicked(object sender, BeginButtonClickedEventArgs e)
        {
            complexTourPartsControlViewModel.ComplexCard_BeginButtonClicked(sender, e);
        }

    }
}
