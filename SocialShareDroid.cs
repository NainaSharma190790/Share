using System;
using ABC.Droid;
using Android.Content;
using Android.Content.PM;
using System.Collections.Generic;
using Xamarin.Forms;
using Android.Content.Res;
using Android.OS;
using Android.App;

[assembly: Xamarin.Forms.Dependency(typeof(SocialShare))]

namespace ABC
{
	public class SocialShare : ISocialShare
	{
		public void OnFacebook(Action<bool> OnFacebookNotFound, string data)
		{
			String fullUrl = "https://m.facebook.com/sharer.php?u=..";

			if (!IsAvailable("com.facebook.android"))
			{
				if (OnFacebookNotFound != null)
					OnFacebookNotFound(true);
				return;
			}
			#region Code

			try
			{
				Intent shareIntent = new Intent(global::Android.Content.Intent.ActionSend);
				shareIntent.SetType("text/plain");
				shareIntent.PutExtra(Intent.ExtraText, data);
				PackageManager pm = Forms.Context.PackageManager;
				IList<ResolveInfo> activityList = pm.QueryIntentActivities(shareIntent, PackageInfoFlags.Activities);
				foreach (ResolveInfo app in activityList)
				{
					if ((app.ActivityInfo.Name).Contains("facebook"))
					{
						ActivityInfo activity = app.ActivityInfo;
						ComponentName name = new ComponentName(activity.ApplicationInfo.PackageName, activity.Name);
						shareIntent.AddCategory(Intent.CategoryLauncher);
						shareIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded);
						shareIntent.SetComponent(name);
						Forms.Context.StartActivity(shareIntent);
						break;
					}
				}
			}
			catch (Exception e)
			{
				Intent i = new Intent(Intent.ActionView);
				i.SetData(global::Android.Net.Uri.Parse(fullUrl));
				Forms.Context.StartActivity(i);
			}

			#endregion
		}

		public void OnTwitter(Action<bool> OnTwitterNotFound, string data)
		{
			String fullUrl = "https://m.twitter.com/sharer.php?u=..";

			if (!IsAvailable("com.twitter.android"))
			{
				if (OnTwitterNotFound != null)
					OnTwitterNotFound(true);
				return;
			}
			#region Code

			try
			{
				Intent shareIntent = new Intent(global::Android.Content.Intent.ActionSend);
				shareIntent.SetType("text/plain");
				shareIntent.PutExtra(Intent.ExtraText, data);

				PackageManager pm = Forms.Context.PackageManager;
				IList<ResolveInfo> activityList = pm.QueryIntentActivities(shareIntent, PackageInfoFlags.Activities);
				foreach (ResolveInfo app in activityList)
				{
					if ((app.ActivityInfo.Name).Contains("twitter"))
					{
						ActivityInfo activity = app.ActivityInfo;
						ComponentName name = new ComponentName(activity.ApplicationInfo.PackageName, activity.Name);
						shareIntent.AddCategory(Intent.CategoryLauncher);
						shareIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ResetTaskIfNeeded);
						shareIntent.SetComponent(name);
						Forms.Context.StartActivity(shareIntent);
						break;
					}
				}

			}
			catch (Exception e)
			{
				Intent i = new Intent(Intent.ActionView);
				i.SetData(global::Android.Net.Uri.Parse(fullUrl));
				Forms.Context.StartActivity(i);
			}

			#endregion
		}

		public bool IsAvailable(string pkgName)
		{
			try
			{
				ApplicationInfo info = Forms.Context.PackageManager.GetApplicationInfo(pkgName, 0);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}