
using System;
using ZoHolyBible.iOS;
using Foundation;
using UIKit;
using Social;


[assembly: Xamarin.Forms.Dependency(typeof(SocialShare))]

namespace ZoHolyBible.iOS
{
	public class SocialShare: ISocialShare
	{
		public void OnFacebook(Action<bool> OnFacebookNotFound, string data)
		{		
			String fullUrl = "https://m.facebook.com/sharer.php?u=..";

			try
			{
				var FBComposer = SLComposeViewController.FromService(SLServiceType.Facebook);
				FBComposer.SetInitialText(textToShare);
			
				UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(FBComposer, true, null);
			}
			catch (Exception e)
			{
				UIApplication.SharedApplication.OpenUrl (new NSUrl (fullUrl));
			}
		}

		public void OnTwitter(Action<bool> OnTwitterNotFound, string data)
		{		
			String fullUrl = "https://m.twitter.com/sharer.php?u=..";

			try
			{
				var TWComposer = SLComposeViewController.FromService(SLServiceType.Twitter);
				TWComposer.SetInitialText(textToShare);
			
				UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(TWComposer, true, null);
			}
			catch (Exception e)
			{
				UIApplication.SharedApplication.OpenUrl (new NSUrl (fullUrl));
			}

		}
	}
}