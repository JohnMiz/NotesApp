using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
	 public class UserAuth
	 {
		  private User _User;

		  public UserAuth(User user)
		  {
			   _User = user;
		  }

		  public bool VerifyCredentials()
		  {
			   if (string.IsNullOrEmpty(_User.Username) || string.IsNullOrEmpty(_User.Password))
			   {
					return false;
			   }

			   return true;
		  }

		  public bool VerifyRegisterInfo()
		  {
			   if (string.IsNullOrEmpty(_User.Username) ||
					string.IsNullOrEmpty(_User.Password) ||
					string.IsNullOrEmpty(_User.Email) ||
					string.IsNullOrEmpty(_User.Lastname) ||
					string.IsNullOrEmpty(_User.Name))
			   {
					return false;
			   }

			   return true;
		  }
	 }
}
