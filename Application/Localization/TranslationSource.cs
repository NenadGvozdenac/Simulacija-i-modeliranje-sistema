﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BookingApp.Application.Localization;

class TranslationSource : INotifyPropertyChanged
{
    private static readonly TranslationSource instance = new TranslationSource();
    public static TranslationSource Instance
    {
        get { return instance; }
    }

    private readonly ResourceManager resourceManager = BookingApp.Properties.Resources.ResourceManager;
    private CultureInfo currentCulture = null;

    public string this[string key]
    {
        get
        {
            if (key == null) return "";
            string retVal = resourceManager.GetString(key, currentCulture);
            return retVal;
        }
    }

    public CultureInfo CurrentCulture
    {
        get { return currentCulture; }
        set
        {
            if (currentCulture != value)
            {
                currentCulture = value;
                var @event = PropertyChanged;
                if (@event != null)
                {
                    @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                }
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

public class LocExtension : Binding
{
    public LocExtension(string name)
        : base("[" + name + "]")
    {
        Mode = BindingMode.OneWay;
        Source = TranslationSource.Instance;
    }
}