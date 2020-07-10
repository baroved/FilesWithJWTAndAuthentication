using ClientLib.Infra;
using ClientLib.Services;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using UI;

using UI.ViewModel;
using UI.Views;

namespace FinalProjectLiron.ViewModel
{
   
    public class ViewModelLocator
    {
       
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var nav = new NavigationService();
            nav.Configure("MainPage", typeof(MainPage));
            nav.Configure("RegisterPage", typeof(RegisterPage));
            nav.Configure("FilesPage", typeof(FilesPage));
            nav.Configure("DownloadFiles", typeof(DownloadFiles));
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<IHttpService, HttpService>();
            SimpleIoc.Default.Register<IUserService,UserService>();
            SimpleIoc.Default.Register<IFilesService, FilesService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<FilesViewModel>();
            SimpleIoc.Default.Register<DownloadFilesViewModel>();
            SimpleIoc.Default.Register<IDialogService,DialogService>();
        }

        public FilesViewModel FilesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FilesViewModel>();
            }
        }

        public DownloadFilesViewModel DownloadFilesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DownloadFilesViewModel>();
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public RegisterViewModel RegisterVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegisterViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}