using App1.PrismLite.MVVM;
using App1.PrismLite.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    class Page2ViewModel : ViewModelBase
    {
        public Command CommandBtn => new Command(async () =>
        {
            await NavigationService.NavigateAsync("/Page3");
        });
        public Page2ViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }

    }
}
