using App1.PrismLite.MVVM;
using App1.PrismLite.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class Page1ViewModel : ViewModelBase
    {
        private string _Stringtext;
        public string Stringtext
        {
            get { return _Stringtext; }
            set { SetProperty(ref _Stringtext, value); }
        }

        public Command CommandBtn => new Command(async() =>
          {
              await NavigationService.NavigateAsync("/Page2");
          });

        public Page1ViewModel(INavigationService navigationService) : base(navigationService)
        {
            Stringtext = "Hiệu khùng";
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {


            return base.OnNavigationAsync(parameters, navigationType);
        }
    }
}
