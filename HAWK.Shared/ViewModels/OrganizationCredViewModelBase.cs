using HAWK.Shared.Services.AppConfigService;
using ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using DynamicData;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive;
using System.Linq;

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
        private ICollection<OrganizationCred> organizationCredsFromJson;
        ILocalFileService LocalFilesService { get; }
        private SourceCache<OrganizationCred, string> OrganizationsCredCache;
        public ReadOnlyObservableCollection<OrganizationCredViewModel> Items;
        public ReactiveCommand<Unit, Unit> LoadOrganizationCred { get; }

        private OrganizationCredViewModel(OrganizationCredViewModel parent)
        {
            DeleteOganizationCredCommand = parent.DeleteOganizationCredCommand;
        }

        public OrganizationCredViewModel()
        {
            LocalFilesService = Startup.ServiceProvider.GetService<ILocalFileService>();
            OrganizationsCredCache = new SourceCache<OrganizationCred, string>(org => org.Name);
            OrganizationsCredCache.Connect()
                             .Transform(OrganizationsCred =>
                                        new OrganizationCredViewModel(this)
                                        {
                                            Name = OrganizationsCred.Name,
                                            AccessToken = OrganizationsCred.AccessToken
                                        })
                             .ObserveOn(RxApp.MainThreadScheduler)
                             .Bind(out Items)
                             .Subscribe();

            LoadOrganizationCred = ReactiveCommand.Create(() => OrganizationsCredCache.AddOrUpdate(organizationCredsFromJson = LocalFilesService.ReadOrgTok()));

            var canExecute = this.WhenAnyValue(
           x => x.Name, x => x.AccessToken,
           (orgName, tocken) =>
           !string.IsNullOrEmpty(orgName) &&
           !string.IsNullOrEmpty(tocken));

            SaveCommand = ReactiveCommand.Create(SaveNewOrganization, canExecute);
            CancelCommand = ReactiveCommand.Create(Cancel_Clicked, canExecute);
            DeleteOganizationCredCommand = ReactiveCommand.Create<OrganizationCredViewModel>(Delete);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        private void SaveNewOrganization()
        {
            if (!organizationCredsFromJson.Any(org => org.Name == Name))
            {
                organizationCredsFromJson.Add(new OrganizationCred() { Name = Name, AccessToken = AccessToken });
                LocalFilesService.SaveOrgTok(organizationCredsFromJson);
                OrganizationsCredCache.AddOrUpdate(organizationCredsFromJson);
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

        public ReactiveCommand<OrganizationCredViewModel, Unit> DeleteOganizationCredCommand { get; }
        private void Delete(OrganizationCredViewModel organization)
        {
            var credentialToRemove = organizationCredsFromJson.FirstOrDefault(orgCred => orgCred.Name == organization.Name);
            if (credentialToRemove != null)
            {
                organizationCredsFromJson.Remove(credentialToRemove);
                LocalFilesService.SaveOrgTok(organizationCredsFromJson);
                OrganizationsCredCache.Remove(credentialToRemove.Name);
            }
        }
    }
}