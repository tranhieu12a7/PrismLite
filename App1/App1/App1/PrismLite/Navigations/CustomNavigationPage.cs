using App1.PrismLite.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;
using Page = Xamarin.Forms.Page;

namespace App1.PrismLite.Navigations
{
    public class CustomNavigationPage : NavigationPage
    {
        public INavigationService NavigationService;
        public CustomNavigationPage(Page root) : base(root)
        {
            BarTextColor = Color.White;
            BarBackgroundColor = Color.Orange;
            SetBackButtonTitle(this, "PageBack");
            NavigationService = new NavigationService();
            On<iOS>()
                .SetStatusBarTextColorMode(StatusBarTextColorMode.MatchNavigationBarTextLuminosity);
            this.Popped += CustomNavigationPage_Popped;
        }

       

        private async void CustomNavigationPage_Popped(object sender, NavigationEventArgs e)
        {

            await NavigationService.NavigateBackAsync(false);
            if (e.Page.BindingContext is ViewModelBase vm)
            {
                vm.Destroy();
            }
            //if (this.Navigation.NavigationStack.LastOrDefault() is Page view)
            //if (view.BindingContext is ViewModelBase vm)
            //{
            //    await vm.OnNavigationAsync(new NavigationParameters(), NavigationType.Back);
            //}
        }


       

    }
}
