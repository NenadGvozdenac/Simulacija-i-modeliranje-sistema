using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.GuideViewModels;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Application.Commands
{
    internal class AddTourImageCommand : ICommand
    {
        private AddTourWindowViewModel addTourWindowViewModel;

        public AddTourImageCommand(AddTourWindowViewModel _addTourWindowViewModel)
        {
            this.addTourWindowViewModel = _addTourWindowViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var path = TourImageService.GetInstance().GetImageFromUser();
            if (path != null)
            {
                if (!ImageAlreadyAdded(path))
                {
                    addTourWindowViewModel.Images.Add(new TourImage(path));
                    addTourWindowViewModel.ImageURL = path;
                    
                }
            }
        }

        public bool ImageAlreadyAdded(string path)
        {
            return addTourWindowViewModel.Images.Any(image => image.Path == path);
        }















    }
}
