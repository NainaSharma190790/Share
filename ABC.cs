using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using System.IO;
using System.Xml;

namespace ABC
{
	public class HomeViewModel: BaseViewModel
	{		
		private INavigation _navigation; 		

		public HomeViewModel(INavigation navigation)
		{
			_navigation = navigation;
		}
		#region Loading indicator

		private bool isLoading;
		public bool IsLoading
		{
			get { return isLoading; }
			set
			{
				isLoading = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region Command for Chapters button

		public ICommand ChaptersClick
		{
			get
			{
				return new Command (async () =>
					{
						IsLoading=true;
						_navigation.PushModalAsync(new ChapterListView ());
						IsLoading=false;
					}
				);
			}
		}

		#endregion

		#region Command for About Us button

		public ICommand AboutUsClick
		{
			get
			{
				return new Command (async () =>
					{
						IsLoading=true;
						_navigation.PushModalAsync(new AboutUsView ());
						IsLoading=false;
					}
				);
			}
		}
		#endregion

		#region Command for Share button 

		private Command _checkInternet;
		//For check internet connection.
		public Command CheckInternet
		{
			get
			{
				return _checkInternet ?? (_checkInternet = new Command(async () => await CheckInternetConnection()));
			}
		}
		//For share application link on facebook and twitter.
		string shareMsg="I am using this app to read bible in Zo Language. You can download it from the link";
		private async Task CheckInternetConnection()
		{			
			try

			{				
				if (!IsNetworkConnected) 
				{
					IsLoading=true;					
					ZoHolyBibleApp.Instance.Alert("Alert", AppResources.NoInternet,AppResources.OK);

				}
				else
				{
					IsLoading=true;					

					var action = await ZoHolyBibleApp.Instance.DisplayActionSheet ("Share ZoHolyBible", "Cancel","", "Facebook","Twitter");

					switch (action)
					{
					case "Facebook": 
						try
						{
							DependencyService.Get<ISocialShare>().OnFacebook(OnFacebookNotFound,shareMsg);
							IsLoading=false;					

						}
						catch (Exception ex)
						{
							ZoHolyBibleApp.Instance.Alert("Facebook application is not exist.", "Facebook", "Ok");
							IsLoading=false;					

						}			
						break;
					case "Twitter":
						try
						{
							DependencyService.Get<ISocialShare>().OnTwitter(OnTwitterNotFound,shareMsg);
							IsLoading=false;					

						}
						catch (Exception ex)
						{
							ZoHolyBibleApp.Instance.Alert("Twitter application is not exist.", "Twitter", "Ok");
							IsLoading=false;					

						}	
						break;
					}

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occurred : {0}", ex.Message);
			}
			IsLoading=false;					

		}
		#endregion 
		private void OnTwitterNotFound(bool obj)
		{
			if (obj) 
			{
			}
		}
		private void OnFacebookNotFound(bool obj)
		{
			if (obj) 
			{
			}
		}

	}
}

