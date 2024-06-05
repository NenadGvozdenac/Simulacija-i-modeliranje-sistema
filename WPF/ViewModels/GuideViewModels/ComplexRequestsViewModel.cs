using BookingApp.Domain.Models;
using BookingApp.WPF.Views.GuideViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class ComplexRequestsViewModel
    {

       public ComplexRequests complexRequests {  get; set; }

       public ConplexRequestsControl complexRequestControl { get; set; }

       public User user { get; set; }

       public ComplexRequestsViewModel(ComplexRequests _complexRequests, User _user)
        {
            user = _user;
            complexRequests = _complexRequests;
            complexRequestControl = new ConplexRequestsControl(user);
            complexRequestControl.Width = 1200;
            complexRequests.Content = complexRequestControl;
        }




    }
}
