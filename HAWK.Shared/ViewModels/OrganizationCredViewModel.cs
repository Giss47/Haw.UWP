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

        ILocalFileService LocalFilesService { get; }
        private SourceCache<OrganizationCred, string> OrganizationsCred;
        public ReadOnlyObservableCollection<OrganizationCredViewModel> Items;
        public ReactiveCommand<Unit, Unit> LoadOrganizationCred { get; }
        public OrganizationCredViewModel()
        {
            LocalFilesService = Startup.ServiceProvider.GetService<ILocalFileService>();
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
            LoadOrganizationCred = ReactiveCommand.Create(
                () => OrganizationsCred.AddOrUpdate(LocalFilesService.ReadOrgTok())
                );
        }
    }
}