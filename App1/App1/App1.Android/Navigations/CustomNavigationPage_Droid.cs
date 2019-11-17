using Android.App;
using Android.Content;
using App1.PrismLite.MVVM;
using App1.PrismLite;
using App1.PrismLite.Navigations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using static Android.Hardware.Camera;
using AppCompToolbar = Android.Support.V7.Widget.Toolbar;
using System;
using Android.Views.Accessibility;
using Android.Support.V4.App;

[assembly: ExportRenderer(typeof(App1.PrismLite.Navigations.CustomNavigationPage), typeof(App1.Droid.Navigations.CustomNavigationPage_Droid))]

namespace App1.Droid.Navigations
{
    public class CustomNavigationPage_Droid : NavigationPageRenderer
    {
        public AppCompToolbar toolbar;
        public Activity context;
        protected Xamarin.Forms.Application CurrentApplication => Xamarin.Forms.Application.Current;
        public CustomNavigationPage_Droid(Context context) : base(context)
        {
           
        }


        protected override void SetupPageTransition(Android.Support.V4.App.FragmentTransaction transaction, bool isPush)
        {
            base.SetupPageTransition(transaction, isPush);
        }

        protected override Task<bool> OnPushAsync(Page view, bool animated)
        {
            var retVal = base.OnPushAsync(view, animated);
            aaa();
            return retVal;
        }
        public void aaa()
        {
            context = (Activity)Forms.Context;
            toolbar = context.FindViewById<AppCompToolbar>(Droid.Resource.Id.toolbar);

            if (toolbar != null)
            {
                if (toolbar.NavigationIcon != null)
                {
                    toolbar.NavigationIcon = Android.Support.V7.Content.Res.AppCompatResources.GetDrawable(context, Resource.Drawable.back);
                    toolbar.TitleMarginStart = 10;
                }
            }
        }
       
    }
}