using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;
using System.IO;
using System.Xml;

namespace ABC
{
	public class abc:BaseViewModel
	{		
		
		#region Command for Share button 

		
					IsLoading=true;					

					var action = await App.Instance.DisplayActionSheet ("Share app","Cancel","", "Facebook","Twitter");

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
							App.Instance.Alert("Facebook application is not exist.", "Facebook", "Ok");
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
							App.Instance.Alert("Twitter application is not exist.", "Twitter", "Ok");
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

