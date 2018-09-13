using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesApp.Model;

namespace NotesApp.UnitTests
{
	 [TestClass]
	 public class RegisterUnitTest
	 {

		  [TestMethod]
		  public void VerifyRegisterInfo_AllFieldAreFilled_ReturnsTrue()
		  {
			   // Arrange
			   var user = new User { Username="test", Password="test", Email="test",Lastname="test", Name="test"};
			   var userAuth = new UserAuth(user);

			   // Act

			   var result = userAuth.VerifyRegisterInfo();

			   // Assert
			   Assert.IsTrue(result);
		  }

		  [TestMethod]
		  public void VerifyRegisterInfo_NotAllFieldAreFilled_ReturnsFalse()
		  {
			   // Arrange
			   var user = new User { Username = "test", Password = "test", Email = "test", Lastname = "test"};
			   var userAuth = new UserAuth(user);

			   // Act

			   var result = userAuth.VerifyRegisterInfo();

			   // Assert
			   Assert.IsFalse(result);
		  }
	 }
}
