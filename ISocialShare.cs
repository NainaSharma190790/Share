using System;

namespace ABC

{
	public interface ISocialShare
	{		
		void OnFacebook(Action<bool> NoFacebookNotFound,string data);

		void OnTwitter(Action<bool> NoTwitterNotFound,string data);
	}
}

