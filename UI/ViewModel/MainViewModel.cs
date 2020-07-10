using ClientLib.Infra;
using ExtendLib.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;
using UI;
using UI.ActiveUser;
using Windows.UI.Popups;

namespace FinalProjectLiron.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        public ICommand ShowRegisterPage { get; set; }
        public ICommand ShowFilesPage { get; set; }
        private readonly INavigationService _navService;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public MainViewModel(IUserService userService, INavigationService navService, IDialogService dialogService)
        {
            _userService = userService;
            _navService = navService;
            _dialogService = dialogService;
            InitShowRegisterPage();
            InitLogin();


        }
        public void InitLogin()
        {
            ShowFilesPage = new RelayCommand(async () =>
            {
                RaisePropertyChanged(UserName);
                RaisePropertyChanged(Password);
                UserAuthenticationInfo userInfo = new UserAuthenticationInfo() { UserName = UserName, Password = Password };
                var user = await _userService.Login(userInfo);
                if (user != null && !string.IsNullOrEmpty(user.UserToken))
                {
                    ActiveUserInfo.UserInfo = new UserAuthenticationInfo() { Id = user.Id, UserName = user.UserName, EmailAddress = user.EmailAddress, UserToken = user.UserToken };
                    await _dialogService.ShowMessage("You have successfully Connected!", "Login Page", "Ok", null);
                    _navService.NavigateTo("FilesPage");

                }
                else
                {
                    await _dialogService.ShowError("UserName or Password is not correct", "Error!!", "OK", null);
                }
                UserName = "";
                Password = "";
                RaisePropertyChanged(() => UserName);
                RaisePropertyChanged(() => Password);

            });
        }
        public void InitShowRegisterPage()
        {
            ShowRegisterPage = new RelayCommand(() =>
            {
                _navService.NavigateTo("RegisterPage");
            });
        }
    }
}