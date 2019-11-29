using HAWK.Shared.ViewModels;
using ReactiveUI;
using Microsoft.Extensions.DependencyInjection;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HAWK.Shared.Views
{
    public class OrganizationCredViewBase : ReactivePage<OrganizationCredViewModel> { }
    public sealed partial class OrganizationCredView : OrganizationCredViewBase
    {
        public OrganizationCredView()
        {
            ViewModel = Shared.Startup.ServiceProvider.GetService<OrganizationCredViewModel>();
            this.InitializeComponent();
        }
    }
}