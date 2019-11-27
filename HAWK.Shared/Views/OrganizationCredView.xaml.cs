using HAWK.Shared.ViewModels;
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
    public sealed partial class OrganizationCredView : Page, IViewFor<OrganizationCredViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
       .Register(nameof(ViewModel), typeof(OrganizationCredViewModel), typeof(OrganizationCredView), new PropertyMetadata(null));

        public OrganizationCredView()
        {
            this.InitializeComponent();
            ViewModel = new OrganizationCredViewModel();
        }

        public OrganizationCredViewModel ViewModel
        {
            get => (OrganizationCredViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (OrganizationCredViewModel)value;
        }
    }
}