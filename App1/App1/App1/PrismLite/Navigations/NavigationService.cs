using App1.PrismLite.Autofac;
using App1.PrismLite.MVVM;
using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.PrismLite.Navigations
{
    public class NavigationService : Autofac.AutofacContainerExtension, INavigationService
    {
        protected Application CurrentApplication => Application.Current;
        public async Task NavigateAsync(string Uri)
        {
            await NavigateAsync(Uri, new NavigationParameters());
        }

        public async Task NavigateAsync(string Uri, bool Callback)
        {
            await NavigateAsync(Uri, new NavigationParameters(), Callback);
        }
        public async Task NavigateAsync(string Uri, NavigationParameters parameters, bool Callback = true)
        {
            var sss = Uri.Split('/');
            //int coutPage = 0;
            int coutPage = sss.Count();
         
            if (coutPage > 2)
            {
                foreach (var itenPage in sss)
                {
                    if (!string.IsNullOrEmpty(itenPage))
                    {
                        if (PageNavigationRegistry._pageRegistrationCache.ContainsKey(itenPage))
                        {
                            var typePageViewAndViewModel = PageNavigationRegistry._pageRegistrationCache.Where(x => x.Key == itenPage).FirstOrDefault().Value;
                            await NavigateToAsync(typePageViewAndViewModel.PageTypeView, typePageViewAndViewModel.PageTypeViewModel, parameters, Callback, coutPage);
                        }
                    }
                }
                for (int i = sss.Count() - 1; i >= 0; i--)
                {
                    if (!string.IsNullOrEmpty(sss[i]))
                    {
                        if (PageNavigationRegistry._pageRegistrationCache.ContainsKey(sss[i]))
                        {
                            var typePageViewAndViewModel = PageNavigationRegistry._pageRegistrationCache.Where(x => x.Key == sss[i]).FirstOrDefault().Value;
                            BindingContext_(typePageViewAndViewModel.PageTypeView, parameters, typePageViewAndViewModel.PageTypeViewModel);
                        }
                    }
                }
            }
            else
            {
                foreach (var itenPage in sss)
                {
                    if (!string.IsNullOrEmpty(itenPage))
                    {
                        if (PageNavigationRegistry._pageRegistrationCache.ContainsKey(itenPage))
                        {
                            var typePageViewAndViewModel = PageNavigationRegistry._pageRegistrationCache.Where(x => x.Key == itenPage).FirstOrDefault().Value;
                            await NavigateToAsync(viewType: typePageViewAndViewModel.TypeView, typePageViewAndViewModel.PageTypeViewModel, parameters, Callback, coutPage);
                        }
                    }
                }
            }
        }

        private async void BindingContext_(Page viewType, NavigationParameters parameters, Type viewModelType = null)
        {
            viewType.BindingContext = AutofacContainerExtension.Instance.Resolve(viewModelType);
            if (viewType.BindingContext is ViewModelBase vm)
            {
                await vm.OnNavigationAsync(parameters, NavigationType.New);
            }
        }

        public async Task NavigateToAsync(Page viewType, Type viewModelType, NavigationParameters parameters = null, bool CallBack = true, int CountPage = 0)
        {
            if (parameters == null)
                parameters = new NavigationParameters();
            if (viewType != null)
            {
                CustomNavigationPage.SetTitleIconImageSource(viewType, "ic_youtube.png");
                if (CurrentApplication.MainPage is CustomNavigationPage customNavigation)
                {
                    if (CallBack)
                        await customNavigation.PushAsync(viewType);
                    else
                        CurrentApplication.MainPage = new CustomNavigationPage(viewType);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(viewType);
                }
                if (CountPage == 0)
                {
                    if (viewType.BindingContext is ViewModelBase vm)
                    {
                        await vm.OnNavigationAsync(parameters, NavigationType.New);
                    }
                }
            }
        }
        public async Task NavigateToAsync(Type viewType, Type viewModelType, NavigationParameters parameters = null, bool CallBack = true, int CountPage = 0)
        {
            try
            {
                Page view;
                if (parameters == null)
                    parameters = new NavigationParameters();
                view = FindViewByViewModel( viewType, viewModelType);
                if (view != null)
                {
                    //CustomNavigationPage.SetTitleIconImageSource(view, "ic_youtube.png");
                    if (CurrentApplication.MainPage is CustomNavigationPage customNavigation)
                    {
                        if (CallBack)
                            await customNavigation.PushAsync(view);
                        else
                            CurrentApplication.MainPage = new CustomNavigationPage(view);
                    }
                    else
                    {
                        CurrentApplication.MainPage = new CustomNavigationPage(view);
                    }
                    if (view.BindingContext is ViewModelBase vm)
                    {
                        await vm.OnNavigationAsync(parameters, NavigationType.New);
                    }
                }
            }
            catch (Exception error)
            {
                Debugger.Break();
            }
        }
        public async Task NavigateBackAsync(NavigationParameters parameters, bool CallBack)
        {
            try
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    if (CallBack)
                        await navigationPage.PopAsync();

                    if (PageUtilities.GetOnNavigatedToTargetFromChild(navigationPage.Navigation.NavigationStack.LastOrDefault()) is Page view)
                        if (view.BindingContext is ViewModelBase vm)
                        {
                            await vm.OnNavigationAsync(parameters, NavigationType.Back);
                        }
                }
            }
            catch (Exception error)
            {
                Debugger.Break();
            }
        }

        public async Task NavigateBackToMainPageAsync()
        {
            if (!(CurrentApplication.MainPage is CustomNavigationPage))
                return;

            for (var i = CurrentApplication.MainPage.Navigation.NavigationStack.Count - 2; i > 0; i--)
                CurrentApplication.MainPage?.Navigation.RemovePage(CurrentApplication.MainPage.Navigation
                    .NavigationStack[i]);

            await CurrentApplication.MainPage.Navigation.PopAsync();
        }

        public Task NavigateBackAsync(bool IsCallback = true)
        {
            return NavigateBackAsync(null, IsCallback);
        }

        public Task NavigateToAsync<TView, TViewModel>() where TViewModel : ViewModelBase where TView : Page
        {
            return NavigateToAsync(typeof(TView), typeof(TViewModel), new NavigationParameters());
        }

        //public Task NavigateToAsync<TViewModel>(NavigationParameters parameters) where TViewModel : ViewModelBase
        //{
        //    return NavigateToAsync(typeof(TViewModel), parameters);
        //}

        //public Task NavigateToAsync(Type viewModelType)
        //{
        //    return NavigateToAsync(viewModelType, new NavigationParameters());
        //}



        protected Page FindViewByViewModel( Type viewType, Type viewModelType = null)
        {
            try
            {
                //var viewType = Type.GetType(viewModelType.FullName.Replace("ViewModel", "View"));
                if (viewType == null)
                    throw new Exception($"Mapping type for {viewModelType} is not a page");

                var view = Activator.CreateInstance(viewType) as Page;

                if (view != null)
                {
                    if (viewModelType != null)
                        view.BindingContext = AutofacContainerExtension.Instance.Resolve(viewModelType);
                }

                return view;
            }
            catch (Exception ex)
            {
                Debugger.Break();

                throw;
            }
        }


    }
}
