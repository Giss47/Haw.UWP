using HAWK.Shared.Services.AppConfigService;
using ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DynamicData;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive;
using System.Linq;
using System.Threading.Tasks;

namespace HAWK.Shared.ViewModels
{
    public class OrganizationCredViewModel : ReactiveObject
    {
        private string name;
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        private string accessToken;
        public string AccessToken
        {
            get => accessToken;
            set => this.RaiseAndSetIfChanged(ref accessToken, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }
        private ICollection<OrganizationCred> _organizationCreds;
        ILocalFileService LocalFilesService { get; }
        private SourceCache<OrganizationCred, string> OrganizationsCred;
        public ReadOnlyObservableCollection<OrganizationCredViewModel> Items;
        public ReactiveCommand<Unit, Unit> LoadOrganizationCred { get; }
        public OrganizationCredViewModel()
        {
            LocalFilesService = Startup.ServiceProvider.GetService<ILocalFileService>();
            _organizationCreds = LocalFilesService.ReadOrgTok();
            OrganizationsCred = new SourceCache<OrganizationCred, string>(org => org.Name);
            OrganizationsCred.Connect()
                             .Transform(OrganizationsCred =>
                                        new OrganizationCredViewModel()
                                        {
                                            Name = OrganizationsCred.Name,
                                            AccessToken = OrganizationsCred.AccessToken
                                        })
                             .ObserveOn(RxApp.MainThreadScheduler)
                             .Bind(out Items)
                             .Subscribe();
            LoadOrganizationCred = ReactiveCommand.Create(() => OrganizationsCred.AddOrUpdate(_organizationCreds));

            var canExecute = this.WhenAnyValue(
           x => x.Name, x => x.AccessToken,
           (orgName, tocken) =>
           !string.IsNullOrEmpty(orgName) &&
           !string.IsNullOrEmpty(tocken));

            SaveCommand = ReactiveCommand.Create(SaveNewOrganization, canExecute);
            CancelCommand = ReactiveCommand.Create(Cancel_Clicked, canExecute);
            DeleteOganizationCredCommand = ReactiveCommand.Create(Delete);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        private void SaveNewOrganization()
        {
            if (!_organizationCreds.Any(org => org.Name == Name))
            {
                _organizationCreds.Add(new OrganizationCred() { Name = Name, AccessToken = AccessToken });
                LocalFilesService.SaveOrgTok(_organizationCreds);
                OrganizationsCred.AddOrUpdate(_organizationCreds);
                Name = string.Empty;
                AccessToken = string.Empty;
                message = string.Empty;
            }
            else
            {
                Message = "Organization already exists";
            }
        }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        private void Cancel_Clicked()
        {
            Name = string.Empty;
            AccessToken = string.Empty;
        }

        public ReactiveCommand<Unit, Unit> DeleteOganizationCredCommand { get; }
        private void Delete()
        {
            foreach (var org in _organizationCreds)
            {
                if (org.Name == Name)
                {
                    _organizationCreds.Remove(org);
                }
            }
        }
    }
}