using App1.PrismLite.MVVM;
using App1.PrismLite.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.ViewModels
{
    class Page3ViewModel : ViewModelBase
    {
        public Page3ViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public override Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return base.OnNavigationAsync(parameters, navigationType);
        }
    }
}
