using System;
using System.Security.Claims;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using TaskManager;
using TaskManager.Controllers;
using TaskManager.DTOs;
using TaskManager.Entities;
using TaskManager.IServices;

namespace TaskManagerTest.Controller
{
	public class TaskControllerTests
	{
		private readonly ITaskService _taskService;


		public TaskControllerTests()
		{
			_taskService = A.Fake<ITaskService>();
		}


		[Fact]
		public async Task TaskController_GetTasks_OKAsync()
		{

            var controller = new TaskController(_taskService);

            // Act
            var result = await controller.GetAllTasks();

            // Assert
            Assert.NotNull(result);
            result.Should().BeOfType(typeof(OkObjectResult));
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }


        [Fact]
        public async Task AddTask_WithValidInput_Returns_OkResult()
        {
            // Arrange
            var taskDto = new TaskDto {
                Title = "TaskTest",
                Statut = TaskStatut.open,
                Discription = "test test",
                deadLine = DateTime.UtcNow };

            var userId = Guid.NewGuid();
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", userId.ToString())
            }, "mock"));

            A.CallTo(() => _taskService.AddTask(A<TaskDto>._, A<Guid>._))
                .Returns(Task.FromResult(new ResponseDto { IsSuccess = true }));

            var controller = new TaskController(_taskService)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext
                    {
                        User = fakeUser
                    }
                }
            };

            // Act
            var result = await controller.AddTask(taskDto);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }


        [Fact]
        public async Task DeleteTask_WithValidIdAndUser_Returns_NoContent()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", userId.ToString())
            }, "mock"));

            A.CallTo(() => _taskService.DeleteTask(A<Guid>._, A<Guid>._))
                .Returns(Task.FromResult(new ResponseDto { IsSuccess = true }));

            var controller = new TaskController(_taskService)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext
                    {
                        User = fakeUser
                    }
                }
            };

            // Act
            var result = await controller.DeleteTask(taskId);

            // Assert
            Assert.NotNull(result);
            var noContentResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateTask_WithValidIdAndUser_Returns_OkResult()
        {
            var taskId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var taskDto = new TaskDto { /* Initialize your task DTO object */ };
            var fakeUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("userId", userId.ToString())
            }, "mock"));

            A.CallTo(() => _taskService.UpdateTask(A<Guid>._, A<TaskDto>._, A<Guid>._))
                .Returns(Task.FromResult(new ResponseDto { IsSuccess = true }));

            var controller = new TaskController(_taskService)
            {
                ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext
                    {
                        User = fakeUser
                    }
                }
            };

            // Act
            var result = await controller.UpdateTask(taskId, taskDto);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

    }
}

