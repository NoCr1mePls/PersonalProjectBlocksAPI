using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonalProjectBlocksAPI.Services;

namespace PersonalProjectBlocksAPI.test.Services
{
    [TestClass]
    public class AspNetIdentityAuthenticationServiceTest
    {
        [TestMethod]
        public void GetCurrentAuthenticatedUserID_ReturnsUserId_WhenUserIsAuthenticated()
        {
            // Arrange
            var userId = "12345";
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.User).Returns(claimsPrincipal);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

            var service = new AspNetIdentityAuthenticationService(mockHttpContextAccessor.Object);

            // Act
            var result = service.GetCurrentAuthenticatedUserID();

            // Assert
            Assert.AreEqual(userId, result);
        }

        [TestMethod]
        public void GetCurrentAuthenticatedUserID_ReturnsNull_WhenUserIsNotAuthenticated()
        {
            // Arrange
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(c => c.User).Returns(new ClaimsPrincipal());

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(a => a.HttpContext).Returns(mockHttpContext.Object);

            var service = new AspNetIdentityAuthenticationService(mockHttpContextAccessor.Object);

            // Act
            var result = service.GetCurrentAuthenticatedUserID();

            // Assert
            Assert.IsNull(result);
        }
    }
}