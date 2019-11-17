using App1.iOS.Navigations;
using App1.PrismLite.Navigations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPage_IOS))]
namespace App1.iOS.Navigations
{
    class CustomNavigationPage_IOS : NavigationRenderer
    {
        private readonly Stack<NavigationPage> _navigationPageStack = new Stack<NavigationPage>();
        private NavigationPage CurrentNavigationPage => _navigationPageStack.Peek();

        // Constructor
        public CustomNavigationPage_IOS() : base()
        {
        }
        // Below should be the override and custom methods which will be explained later in this blog.
        // ...

        public void SetImageTitleBackButton(string imageBundleName, string buttonTitle, int horizontalOffset)
        {
            var topVC = this.TopViewController;

            // Create the image back button
            var backButtonImage = new UIBarButtonItem(
                    UIImage.FromBundle(imageBundleName),
                    UIBarButtonItemStyle.Plain,
                    (sender, args) =>
                    {
                        topVC.NavigationController.PopViewController(true);
                    });

            // Create the Text Back Button
            var backButtonText = new UIBarButtonItem(
                buttonTitle,
                UIBarButtonItemStyle.Plain,
                (sender, args) =>
                {
                    topVC.NavigationController.PopViewController(true);
                });

            backButtonText.SetTitlePositionAdjustment(new UIOffset(horizontalOffset, 0), UIBarMetrics.Default);

            // Add buttons to the Top Bar
            UIBarButtonItem[] buttons = new UIBarButtonItem[2];
            buttons[0] = backButtonImage;
            buttons[1] = backButtonText;

            topVC.NavigationItem.LeftBarButtonItems = buttons;
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            var retVal = base.OnPushAsync(page, animated);

            SetImageTitleBackButton("back.png", "", 10);

            return retVal;
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var retVal = base.OnPopViewAsync(page, animated);

            var stack = page.Navigation.NavigationStack;

            var returnPage = stack[stack.Count - 2];

            if (returnPage != null)
            {
                SetImageTitleBackButton("back.png", "", 10);
            }
            return retVal;
        }
    }
}