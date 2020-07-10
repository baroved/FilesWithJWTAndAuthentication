using ClientLib.Infra;
using ExtendLib.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ActiveUser;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

namespace UI.ViewModel
{
    public class DownloadFilesViewModel : ViewModelBase
    {
        public ObservableCollection<MyFile> Files { get; set; }
        public RelayCommand<MyFile> DownloadCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }
        public INavigationService _navService { get; set; }
        private readonly IFilesService _filesService;
        private readonly IDialogService _dialogService;

        public DownloadFilesViewModel(IFilesService filesService, INavigationService navService, IDialogService dialogService)
        {
            _filesService = filesService;
            _navService = navService;
            _dialogService = dialogService;


            CommandDownload();
            BackCommand();
            LoadFilesList();

        }
        public void BackCommand()
        {
            GoBackCommand = new RelayCommand(() =>
            {
                _navService.GoBack();
            });
        }

        public void CommandDownload()
        {
            DownloadCommand = new RelayCommand<MyFile>(async (savedfile) =>
            {

                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

                savePicker.FileTypeChoices.Add("Text Format", new List<string>() { ".txt" });
                savePicker.FileTypeChoices.Add("Winrar", new List<string>() { ".rar" });
                savePicker.FileTypeChoices.Add("Jpg Format", new List<string>() { ".jpg" });
                savePicker.FileTypeChoices.Add("Png Format", new List<string>() { ".png" });

                savePicker.SuggestedFileName = savedfile.Name;
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await storageFolder.CreateFileAsync(savedfile.Name, CreationCollisionOption.ReplaceExisting);

                StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {

                    CachedFileManager.DeferUpdates(file);

                    await FileIO.WriteBytesAsync(file, savedfile.Source);

                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    await _dialogService.ShowMessage("You have successfully Download a File!", "Download Page", "Ok", null);
                }
            }
           );
        }
        public async void LoadFilesList()
        {
            Files = new ObservableCollection<MyFile>(await _filesService.GetAllFiles(ActiveUserInfo.UserInfo.Id));
            RaisePropertyChanged(() => Files);
        }
    }
}
