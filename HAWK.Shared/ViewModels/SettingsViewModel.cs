using HAWK.Shared.Services.AppConfigService;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System.Reactive;

namespace HAWK.Shared.ViewModels
{
    public class SettingsViewModel : ReactiveObject
    {
        private ILocalFileService LocalFileService { get; }
        private string currentUri;
        public SettingsViewModel()
        {
            LocalFileService = Startup.ServiceProvider.GetService<ILocalFileService>();
            Uri = LocalFileService.ReadSettings().BaseUrl;
            currentUri = Uri;

            var canExecute = this.WhenAnyValue(
            x => x.Uri, x => x.Key,
            (userName, password) =>
            !string.IsNullOrEmpty(userName) &&
            !string.IsNullOrEmpty(password));

            SaveCommand = ReactiveCommand.Create(SaveSettings, canExecute);
            CancelCommand = ReactiveCommand.Create(Cancel_Clicked, canExecute);
        }
        private string uri;
        public string Uri
        {
            get => uri;
            set => this.RaiseAndSetIfChanged(ref uri, value);
        }

        private string key;
        public string Key
        {
            get => key;
            set => this.RaiseAndSetIfChanged(ref key, value);
        }

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        private void SaveSettings()
        {
            LocalFileService.SaveSettings(Uri, Key);
            currentUri = Uri;
            key = string.Empty;
        }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        public void Cancel_Clicked()
        {
            Key = string.Empty;
            Uri = currentUri;
        }
    }
}