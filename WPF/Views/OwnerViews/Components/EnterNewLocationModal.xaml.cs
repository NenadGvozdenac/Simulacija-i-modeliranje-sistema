﻿using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.WPF.ViewModels.OwnerViewModels.Components;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

/// <summary>
/// Interaction logic for EnterNewLocationModal.xaml
/// </summary>
public partial class EnterNewLocationModal : UserControl
{
    private readonly AddAccommodationViewModel viewModel;

    public EnterNewLocationModal(AddAccommodationViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;

        EnterLocationViewModel enterLocationViewModel = new EnterLocationViewModel(viewModel);
        DataContext = enterLocationViewModel;
    }
}