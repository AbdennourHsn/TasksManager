using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Entities;
using TaskManager.IServices;

namespace TaskManager.Controllers
{

    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
		private readonly ITaskService _taskService;

		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}

        [AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult> GetAllTasks()
		{
            var response = await _taskService.GetAllTasks();
            if (!response.IsSuccess)
                return StatusCode(response.StatusCode, response);
            return Ok(response);
		}

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSingleTask(Guid id)
        {
            var response = await _taskService.GetSingleTask(id);
            if (!response.IsSuccess)
                return StatusCode(response.StatusCode , response);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("assigned")]
        public async Task<ActionResult> GetAssignedTasks()
        {
            var guidStr = User.FindFirstValue("userId");
            if (Guid.TryParse(guidStr, out Guid currUserId))
            {
                var response = await _taskService.GetTaskByAssignedUserId(currUserId);
                if (!response.IsSuccess)
                    return StatusCode(response.StatusCode, response);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet("users/{userId}")]
        public async Task<ActionResult> GetAssignedTasksByUserId(Guid userId)
        {
            var response = await _taskService.GetTaskByAssignedUserId(userId);
            if (!response.IsSuccess)
                return StatusCode(response.StatusCode, response);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] TaskDto taskDto)
        {
            var guidStr = User.FindFirstValue("userId");
            if (Guid.TryParse(guidStr, out Guid currUserId))
            {
                var response = await _taskService.AddTask(taskDto, currUserId);
                if (!response.IsSuccess)
                    return StatusCode(response.StatusCode, response);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(Guid id ,[FromBody] TaskDto request)
        {
            var guidStr = User.FindFirstValue("userId");
            if (Guid.TryParse(guidStr, out Guid currUserId))
            {
                var response = await _taskService.UpdateTask(id, request, currUserId);
                if (!response.IsSuccess)
                    return StatusCode(response.StatusCode, response);
                return Ok(response);
            }
            else
                return BadRequest();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var userIdStr = User.FindFirstValue("userId");
            if (Guid.TryParse(userIdStr, out Guid userId))
            {
                var response = await _taskService.DeleteTask(id, userId);
                if (!response.IsSuccess)
                    return StatusCode(response.StatusCode, response);
                return NoContent();
            }
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPatch("{taskId}/users/{userId}")]
        public async Task<ActionResult> AssignTaskToUser(Guid taskId , Guid userId)
        {
            var guidStr = User.FindFirstValue("userId");
            if (Guid.TryParse(guidStr, out Guid currUserId))
            {
                var response = await _taskService.AssignTask(taskId, userId, currUserId);
                if (!response.IsSuccess)
                    return StatusCode(response.StatusCode, response);
                return Ok(response);
            }
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateTaskStatus(Guid id , TaskStatut status)
        {
            var guidStr = User.FindFirstValue("userId");
            if (Guid.TryParse(guidStr, out Guid currUserId))
            {
                var response = await _taskService.UpdateTaskStatus(id, status , currUserId);
                if (!response.IsSuccess)
                    return StatusCode(response.StatusCode, response);
                return Ok(response);
            }
            else
                return BadRequest();
        }
    }
}

