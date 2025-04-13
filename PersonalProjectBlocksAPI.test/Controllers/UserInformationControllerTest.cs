using Moq;
using SmartHealth.WebApi.Controllers;
using SmartHealth.WebApi.Interfaces.Services;

namespace PersonalProjectBlocksAPI.test.Controllers
{
    [TestClass]
    public class UserInformationControllerTest
    {
        private Mock<IAuthenticationService> _authServiceMock;
        private UserInformationController _controller;

        [TestInitialize]
        public void Setup()
        {
            // Arrange: Initialize the mock and the controller
            _authServiceMock = new Mock<IAuthenticationService>();
            _controller = new UserInformationController(_authServiceMock.Object);
        }

        [TestMethod]
        public void GetUser_ShouldReturnAuthenticatedUserId()
        {
            // Arrange: Set up the mock to return a specific user ID
            var expectedUserId = "test-user-id";
            _authServiceMock.Setup(auth => auth.GetCurrentAuthenticatedUserID()).Returns(expectedUserId);

            // Act: Call the method
            var result = _controller.GetUser();

            // Assert: Verify the result matches the expected user ID
            Assert.AreEqual(expectedUserId, result);

            // Verify that the mock method was called exactly once
            _authServiceMock.Verify(auth => auth.GetCurrentAuthenticatedUserID(), Times.Once);
        }
    }
}
