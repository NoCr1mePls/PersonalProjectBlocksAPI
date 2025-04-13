using Moq;
using SmartHealth.WebApi.Controllers;
using SmartHealth.WebApi.Interfaces.Services;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace PersonalProjectBlocksAPI.test.Controllers
{
    [TestClass]
    public class DataControllerTest
    {
        private Mock<IDatabaseRepository> _mockRepo;
        private Mock<IAuthenticationService> _mockAuth;
        private DataController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IDatabaseRepository>();
            _mockAuth = new Mock<IAuthenticationService>();
            _controller = new DataController(_mockRepo.Object, _mockAuth.Object);
        }

        [TestMethod]
        public async Task StoreNewEnvironment_ReturnsCreated_WhenSuccessful()
        {
            // Arrange
            var env = new Environment2DDto { Id = Guid.NewGuid(), Name = "TestEnv", UserId = "User1" };
            _mockAuth.Setup(auth => auth.GetCurrentAuthenticatedUserID()).Returns("User1");
            _mockRepo.Setup(repo => repo.InsertNewEnvironment(env, "User1")).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.StoreNewEnvironment(env);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task StoreNewEnvironment_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var env = new Environment2DDto { Id = Guid.NewGuid(), Name = "TestEnv", UserId = "User1" };
            _mockAuth.Setup(auth => auth.GetCurrentAuthenticatedUserID()).Returns("User1");
            _mockRepo.Setup(repo => repo.InsertNewEnvironment(env, "User1")).Throws(new Exception("Error"));

            // Act
            var result = await _controller.StoreNewEnvironment(env);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetEnvironments_ReturnsOk_WhenDataExists()
        {
            // Arrange
            var environments = new List<Environment2DDto>
            {
                new Environment2DDto { Id = Guid.NewGuid(), Name = "Env1", UserId = "User1" }
            };
            _mockAuth.Setup(auth => auth.GetCurrentAuthenticatedUserID()).Returns("User1");
            _mockRepo.Setup(repo => repo.GetEnvironment2D("User1")).ReturnsAsync(environments);

            // Act
            var result = await _controller.GetEnvironments();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetEnvironments_ReturnsBadRequest_WhenNoDataExists()
        {
            // Arrange
            _mockAuth.Setup(auth => auth.GetCurrentAuthenticatedUserID()).Returns("User1");
            _mockRepo.Setup(repo => repo.GetEnvironment2D("User1")).ReturnsAsync((IEnumerable<Environment2DDto>)null);

            // Act
            var result = await _controller.GetEnvironments();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task StoreWorld_ReturnsCreated_WhenSuccessful()
        {
            // Arrange
            var objects = new Object2DDto[]
            {
                new Object2DDto { Id = Guid.NewGuid(), PrefabId = 1, PositionX = 0, PositionY = 0 }
            };
            _mockRepo.Setup(repo => repo.Insert2DObjects(objects, "Env1")).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.StoreWorld(objects, "Env1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }

        [TestMethod]
        public async Task StoreWorld_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var objects = new Object2DDto[]
            {
                new Object2DDto { Id = Guid.NewGuid(), PrefabId = 1, PositionX = 0, PositionY = 0 }
            };
            _mockRepo.Setup(repo => repo.Insert2DObjects(objects, "Env1")).Throws(new Exception("Error"));

            // Act
            var result = await _controller.StoreWorld(objects, "Env1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task GetObjects2D_ReturnsOk_WhenDataExists()
        {
            // Arrange
            var objects = new List<Object2DDto>
            {
                new Object2DDto { Id = Guid.NewGuid(), PrefabId = 1, PositionX = 0, PositionY = 0 }
            };
            _mockRepo.Setup(repo => repo.Get2DObjects("Env1")).ReturnsAsync(objects);

            // Act
            var result = await _controller.GetObjects2D("Env1");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GetObjects2D_ReturnsBadRequest_WhenNoDataExists()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.Get2DObjects("Env1")).ReturnsAsync((IEnumerable<Object2DDto>)null);

            // Act
            var result = await _controller.GetObjects2D("Env1");

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteEnvironment_ReturnsOk_WhenSuccessful()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteEnvironment("Env1")).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteEnvironment("Env1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteEnvironment_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.DeleteEnvironment("Env1")).Throws(new Exception("Error"));

            // Act
            var result = await _controller.DeleteEnvironment("Env1");

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}
