using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Views;
using App1.PrismLite.Autofac;
using App1.PrismLite.Navigations;

namespace App1
{
    public partial class App : Application 
    {
        public App()
        {
            InitializeComponent();


            BuildDependencies();

            InitNavigation();
        }

        private void InitNavigation()
        {
            //AutofacContainerExtension.Instance.Resolve<INavigationService>().NavigateToAsync<Views.Page1View,ViewModels.PageViewModel>();
            //AutofacContainerExtension.Instance.Resolve<INavigationService>().NavigateAsync("/Page1/Page2");
            AutofacContainerExtension.Instance.Resolve<INavigationService>().NavigateAsync("/Page1");
            //MainPage = new AppShell();
        }
        private void BuildDependencies()
        {
            if (AutofacContainerExtension.Instance.Built)
                return;

            // Register dependencies
            AutofacContainerExtension.Instance.RegisterInstance<INavigationService, NavigationService>();
            //AutofacContainerExtension.Instance.RegisterViewModels();
            AutofacContainerExtension.Instance.RegisterForNavigation<Views.Page1, ViewModels.Page1ViewModel>();
            AutofacContainerExtension.Instance.RegisterForNavigation<Views.Page2, ViewModels.Page2ViewModel>();
            AutofacContainerExtension.Instance.RegisterForNavigation<Views.Page3, ViewModels.Page3ViewModel>();

            AutofacContainerExtension.Instance.Build();


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
