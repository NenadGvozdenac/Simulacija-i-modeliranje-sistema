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
    internal class RemoveTourImageCommand : ICommand
    {
        private AddTourWindowViewModel addTourWindowViewModel;

        public RemoveTourImageCommand(AddTourWindowViewModel _addTourWindowViewModel)
        {
            this.addTourWindowViewModel = _addTourWindowViewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return addTourWindowViewModel.Images.Count > 0;
        }

        public void Execute(object? parameter)
        {
            var currentImage = addTourWindowViewModel.Images.FirstOrDefault(image => image.Path == addTourWindowViewModel.ImageURL);
            addTourWindowViewModel.Images.Remove(currentImage);
            addTourWindowViewModel.ImageURL = addTourWindowViewModel.Images.FirstOrDefault()?.Path;
        }

    }
}
