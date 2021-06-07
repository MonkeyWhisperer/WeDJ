using WeDJ.Models;
using System;

namespace WeDJ.Services.Contracts
{
	public interface IFacebookManager
	{				
		void Login(Action<FacebookUser, string> onLoginComplete);

		void Logout();
	}
}
