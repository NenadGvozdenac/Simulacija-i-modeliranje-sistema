﻿using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using BookingApp.WPF.DTOs.OwnerDTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.OwnerViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly User user;

    [ObservableProperty]
    private OwnerUserDTO _ownerUserDTO;

    [ObservableProperty]
    private List<string> _languages = new() { "English", "Serbian" };

    [ObservableProperty]
    private string _selectedLanguage;

    public SettingsViewModel(User user)
    {
        OwnerUserDTO = new(user);
        SelectedLanguage = OwnerUserDTO.OwnerInfo.PrefferedLanguage;
        this.user = user;
    }

    public void SetTheme(string v)
    {
        OwnerInfo ownerInfo = OwnerService.GetInstance().GetById(user.Id).Item1;
        ownerInfo.PrefferedTheme = v;
        OwnerService.GetInstance().UpdateOwnerInfo(ownerInfo);
    }

    public void ChangeLanguage()
    {
        OwnerInfo ownerInfo = OwnerService.GetInstance().GetById(user.Id).Item1;
        ownerInfo.PrefferedLanguage = SelectedLanguage;
        OwnerService.GetInstance().UpdateOwnerInfo(ownerInfo);
        // Current application

        App app = (App)System.Windows.Application.Current;

        app.ChangeLanguage(ownerInfo.PrefferedLanguage);
        OwnerUserDTO = new(user);
    }
}
