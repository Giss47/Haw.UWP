using HAWK.Shared.Services.AppConfigService;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace HAWK.Shared.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private string message;
        private ILocalFileService LocalFileService { get; }
        private IAppDataDirectoryService AppDataDirectoryService { get; }
        private ReactiveCommand<Unit, Unit> CreateFilesCommand { get; }

        public MainViewModel(IAppDataDirectoryService appDataDirectoryService, ILocalFileService localFileService)
        {
            LocalFileService = localFileService;
            AppDataDirectoryService = appDataDirectoryService;
            CreateFilesCommand = ReactiveCommand.Create(() =>
            {
                AppDataDirectoryService.CreateDataFiles();
            });
            CreateFilesCommand.Execute().Subscribe();
            CreateFilesCommand.ThrownExceptions.Subscribe((Exception) =>
            {
                Message = Exception.Message;
            });
        }

        public string Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }
    }
}