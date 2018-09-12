using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp.Model;

namespace NotesApp.UnitTests
{
	 [TestClass]
	 public class LoginUnitTest
	 {
		  [TestMethod]
		  public void VerifyCerdantials_UsernameEmpty_ReturesFalse()
		  {
			   // Arrange
			   var user = new User { Password = "test" };
			   var userAuth = new UserAuth(user);

			   // Act 
			   var result = userAuth.VerifyCredentials();

			   // Assert
			   Assert.IsFalse(result);
		  }

		  [TestMethod]
		  public void VerifyCerdantials_PasswordEmpty_ReturesFalse()
		  {
			   // Arrange
			   var user = new User { Username = "test" };
			   var userAuth = new UserAuth(user);

			   // Act 
			   var result = userAuth.VerifyCredentials();

			   // Assert
			   Assert.IsFalse(result);
		  }

		  [TestMethod]
		  public void VerifyCerdantials_UsernameAndPasswordAreFilled_ReturesTrue()
		  {
			   // Arrange
			   var user = new User { Username="Test", Password = "test" };
			   var userAuth = new UserAuth(user);

			   // Act 
			   var result = userAuth.VerifyCredentials();

			   // Assert
			   Assert.IsTrue(result);
		  }


		  [TestMethod]
		  public void VerifyPassword_PasswordIsIncorrect_ReturesFalse()
		  {
			   // Arrange
			   var user = new User { Username = "Test", Password = "test" };
			   var userAuth = new UserAuth(user);

			   // Act 
			   var result = userAuth.VerifyPassword("test1");

			   // Assert
			   Assert.IsFalse(result);
		  }

		  [TestMethod]
		  public void VerifyPassword_PasswordIsCorrect_ReturesTrue()
		  {
			   // Arrange
			   var user = new User { Username = "Test", Password = "test" };
			   var userAuth = new UserAuth(user);

			   // Act 
			   var result = userAuth.VerifyPassword("test");

			   // Assert
			   Assert.IsTrue(result);
		  }

	 }
}
