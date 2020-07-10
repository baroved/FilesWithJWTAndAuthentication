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
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace UI.ViewModel
{
    public class FilesViewModel : ViewModelBase
    {
        public static FileOpenPicker filepicker { get; set; }
        public ICommand UploadFileCommand { get; set; }
        public ObservableCollection<MyFile> FilesList { get; set; }
        private readonly IFilesService _filesService;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        public INavigationService _navService { get; set; }
        public ICommand DownloadPageCommand { get; set; }
        public ICommand DisconnectedCommand { get; set; }
        public FilesViewModel(IFilesService filesService, IUserService userService, INavigationService navService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _navService = navService;
            _userService = userService;
            _filesService = filesService;
            LoadFilesList();
            InitUploadCommand();
            InitDownloadPage();
            InitDisconnectedCommand();
            if (filepicker == null)
            {
                filepicker = new FileOpenPicker();
                filepicker.ViewMode = PickerViewMode.Thumbnail;
                filepicker.FileTypeFilter.Add("*");
            }
        

        }
        public void InitDisconnectedCommand()
        {
            DisconnectedCommand = new RelayCommand(async() =>
              {
                  ActiveUserInfo.UserInfo = null;
                  await _dialogService.ShowMessage("You have successfully Disconnected!", "Login", "Ok", null);
                  _navService.NavigateTo("MainPage");
              }
            );
        }
        public void InitDownloadPage()
        {
            DownloadPageCommand = new RelayCommand(() =>
            {
                _navService.NavigateTo("DownloadFiles");
            }
           );
        }
      public void InitUploadCommand()
        {
            UploadFileCommand = new RelayCommand(async () =>
            {

                StorageFile file = await filepicker.PickSingleFileAsync();
                if (file != null)
                {
                    using (var stream = await file.OpenReadAsync())
                    {
                        var data = new byte[stream.Size];
                        using (var reader = new DataReader(stream))
                        {
                            await reader.LoadAsync((uint)stream.Size);
                            reader.ReadBytes(data);
                        }

                        var newFile = new MyFile()
                        {
                            Name = file.Name,
                            Source = data,
                            UserId = ActiveUserInfo.UserInfo.Id
                        };

                        await _filesService.AddFile(newFile, ActiveUserInfo.UserInfo.UserToken);
                        LoadFilesList();
                        await _dialogService.ShowMessage("You have successfully Upload a File!", "Files Page", "Ok", null);
                    }
                }
            }
        );
        }
        public async void LoadFilesList()
        {
            FilesList = new ObservableCollection<MyFile>(await _filesService.GetAllFiles(ActiveUserInfo.UserInfo.Id));
            RaisePropertyChanged(() => FilesList);
        }
    }
}
