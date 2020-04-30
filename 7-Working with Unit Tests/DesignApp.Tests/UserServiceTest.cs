using DesignApp.Application.Services;
using DesignApp.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesignApp.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly UserRepositoryInMemory _userRepo;

        public UserServiceTest()
        {
            _userRepo = new UserRepositoryInMemory();
        }

        [TestMethod]
        public void FindUserByUserId_ExpectMatchedResult()
        {
            // Arrange
            var userService = new UserService(_userRepo);

            // Act
            var matchedUser = userService.FindUser("smith12");

            // Assert
            Assert.IsNotNull(matchedUser);
            Assert.AreEqual("smith12", matchedUser.UserId);
        }

        [TestMethod]
        public void FindUserByUserId_ExpectNullResult()
        {
            // Arrange
            var userService = new UserService(_userRepo);

            // Act
            var matchedUser = userService.FindUser("------");

            // Assert
            Assert.IsNull(matchedUser);
        }

        [TestMethod]
        public void GetAll_ExpectNonEmptyList()
        {
            // Arrange
            var userService = new UserService(_userRepo);

            // Act
            var userList = userService.GetAllUsers();

            // Assert
            Assert.IsNotNull(userList);
            Assert.IsTrue(userList.Count > 0);
        }

        [TestMethod]
        public void UpdateUser_ExpectChangedOwner()
        {
            // Arrange
            var userService = new UserService(_userRepo);
            var matchedUser = userService.FindUser("smith12");

            matchedUser.Owner = "C";

            // Act
            userService.UpdateUser(matchedUser);

            var newlyUpdatedUser = userService.FindUser("smith12");

            // Assert
            Assert.IsNotNull(newlyUpdatedUser);
            Assert.AreEqual(matchedUser.UserId, newlyUpdatedUser.UserId);
            Assert.AreEqual(matchedUser.Owner, newlyUpdatedUser.Owner);
        }

        [DataTestMethod]
        [DataRow("E")]
        [DataRow("F")]
        [DataRow("G")]
        [DataRow("H")]
        [DataRow("I")]
        [DataRow("G")]
        [DataRow("K")]
        [DataRow("L")]
        [DataRow("M")]
        [DataRow("N")]
        [DataRow("O")]
        [DataRow("P")]
        [DataRow("Q")]
        [DataRow("R")]
        [DataRow("S")]
        [DataRow("T")]
        [DataRow("U")]
        [DataRow("V")]
        [DataRow("W")]
        [DataRow("X")]
        [DataRow("Y")]
        [DataRow("Z")]
        public void UpdateUser_ExpectInvalidOwnerException(string newOwner)
        {
            // Arrange
            var userService = new UserService(_userRepo);
            var matchedUser = userService.FindUser("smith12");

            matchedUser.Owner = newOwner;

            // Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => userService.UpdateUser(matchedUser));
        }
    }
}
