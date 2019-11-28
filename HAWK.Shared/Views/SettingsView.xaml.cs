using HAWK.Shared;
using HAWK.Shared.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HAWK.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsView : Page, IViewFor<SettingsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
       .Register(nameof(ViewModel), typeof(SettingsViewModel), typeof(SettingsView), new PropertyMetadata(null));

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