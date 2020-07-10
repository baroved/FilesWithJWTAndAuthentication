using ClientLib.Infra;
using ExtendLib.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Windows.UI.Popups;

namespace UI.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        public ICommand GoBackCommand { get; set; }
        public ICommand ShowFilesPage { get; set; }
        private readonly IUserService _userService;
        private readonly INavigationService _navService;
        private readonly IDialogService _dialogService;
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string Email { get; set; } = "";

        public RegisterViewModel(IUserService userService, INavigationService navService, IDialogService dialogService)
        {
            _userService = userService;
            _navService = navService;
            _dialogService = dialogService;
            InitRegisterCommand();
            InitGoBackCommand();

        }
        public void InitRegisterCommand()
        {
            ShowFilesPage = new RelayCommand(async () =>
            {
                RaisePropertyChanged(UserName);
                RaisePropertyChanged(Password);
                RaisePropertyChanged(Email);
                UserAuthenticationInfo userInfo = new UserAuthenticationInfo() { UserName = UserName, Password = Password, EmailAddress = Email };
                var ok = await _userService.Register(userInfo);
                if (ok)
                {
                    await _dialogService.ShowMessage("You have successfully registered!", "Register Page", "Ok", null);
                    _navService.GoBack();
                }
                else
                    await _dialogService.ShowError("Something wrong!!", "Error!!", "OK", null);


            });
        }
        public void InitGoBackCommand()
        {
            GoBackCommand = new RelayCommand(() =>
            {
                _navService.GoBack();
            });
        }
    }
}
