using App1.PrismLite.Autofac;
using App1.PrismLite.MVVM.Common;
using App1.PrismLite.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.PrismLite.MVVM
{
    public class ViewModelBase : BindableBase
    {
        private string _title;
        private bool _isBusy;

        protected INavigationService NavigationService { get; }

        //public ViewModelBase()
        //{
        //    NavigationService = AutofacContainerExtension.Instance.Resolve<INavigationService>();
        //}

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, () => RaisePropertyChanged(nameof(IsNotBusy)));
        }

        public bool IsNotBusy => !IsBusy;

        public virtual Task OnNavigationAsync(NavigationParameters parameters, NavigationType navigationType)
        {
            return Task.CompletedTask;
        }
        public virtual void Destroy()
        {

        }
        private async void Back()
        {
            await NavigationService.NavigateBackAsync();
        }
    }
}
