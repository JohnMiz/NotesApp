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
			   return !(string.IsNullOrEmpty(_User.Username) || string.IsNullOrEmpty(_User.Password));
		  }

		  // TODO: Separate each check and give the user proper message
		  public bool VerifyRegisterInfo()
		  {
			   return !(string.IsNullOrEmpty(_User.Username) ||
					string.IsNullOrEmpty(_User.Password) ||
					string.IsNullOrEmpty(_User.Email) ||
					string.IsNullOrEmpty(_User.Lastname) ||
					string.IsNullOrEmpty(_User.Name));
		  }

		  public bool VerifyPassword(string password)
		  {
			   return (_User.Password == password);
		  }
	 }
}
