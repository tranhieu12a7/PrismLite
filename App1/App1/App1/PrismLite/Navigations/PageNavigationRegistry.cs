using App1.PrismLite.MVVM.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.PrismLite.Navigations
{
    public static class PageNavigationRegistry
    {
        public static Dictionary<string, PageType> _pageRegistrationCache = new Dictionary<string, PageType>();

        public static void Register(string name, Type pageTypeView, Type pageTypeViewModel)
        {
            var info = new PageType
            {
                PageTypeView = Activator.CreateInstance(pageTypeView) as Page,
                TypeView = pageTypeView,
                PageTypeViewModel = pageTypeViewModel
            };
            if (!_pageRegistrationCache.ContainsKey(name))
            {
                _pageRegistrationCache.Add(name, info);
            }
        }

    }
    public class PageType : BindableBase
    {
        private Page _PageTypeView;
        public Page PageTypeView { get => _PageTypeView; set => SetProperty(ref _PageTypeView, value); }
        private Type _PageTypeViewModel;
        public Type PageTypeViewModel { get => _PageTypeViewModel; set => SetProperty(ref _PageTypeViewModel, value); }
        private Type _TypeView;
        public Type TypeView { get => _TypeView; set => SetProperty(ref _TypeView, value); }

    }
}
