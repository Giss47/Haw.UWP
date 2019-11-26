using HAWK.Shared;
using HAWK.Shared.Services.AppConfigService;
using HAWK.Shared.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HAWK.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsView : Page, IViewFor<SettingsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
       .Register(nameof(ViewModel), typeof(SettingsViewModel), typeof(SettingsViewModel), new PropertyMetadata(null));
        public SettingsView()
        {
            this.InitializeComponent();
            ViewModel = Startup.ServiceProvider.GetService<SettingsViewModel>();
        }

        public SettingsViewModel ViewModel
        {
            get => (SettingsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SettingsViewModel)value;
        }
    }
}