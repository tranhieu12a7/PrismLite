using App1.PrismLite.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.PrismLite.Navigations
{
    public interface INavigationService
    {
        //Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TView, TViewModel>() where TViewModel : ViewModelBase where TView : Page;

        //Task NavigateToAsync<TViewModel>(NavigationParameters parameters) where TViewModel : ViewModelBase;

        //Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewType, Type viewModelType, NavigationParameters parameters=null,bool CallBack=true, int CountPage = 0);
        Task NavigateAsync(string Uri);
        Task NavigateAsync(string Uri,bool Callback);
        Task NavigateAsync(string Uri,NavigationParameters parameters, bool Callback=true);
        Task NavigateBackAsync(bool IsCallback=true);
        Task NavigateBackAsync(NavigationParameters parameters, bool IsCallback = true);

        Task NavigateBackToMainPageAsync();
    }
}
