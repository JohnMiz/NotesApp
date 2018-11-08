using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using NotesApp.Model;

namespace NotesApp.Services
{
	 public class AzureLoginService : ILoginService
	 {
		  public async Task<User> GetUserFromDB(string username)
		  {
			   var user = (await App.MobileServiceClient.GetTable<User>().ToListAsync()).Where(u => u.Name == username).FirstOrDefault();

			   return user;
		  }

		  public void Login()
		  {
			   throw new NotImplementedException();
		  }
	 }
}
