using System;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Entities;

namespace TaskManager.IServices
{
	public interface ITaskService
	{
        Task<ResponseDto> GetAllTasks();
		Task<ResponseDto> GetSingleTask(Guid id);
		Task<ResponseDto> AddTask(TaskDto task , Guid CurrUserId);
		Task<ResponseDto> UpdateTask(Guid id, TaskDto task ,Guid CurrUserId);
		Task<ResponseDto> DeleteTask(Guid id , Guid CurrUserId);

        Task<ResponseDto> AssignTask(Guid id, Guid ToUser , Guid CurrUserId);
		Task<ResponseDto> UpdateTaskStatus(Guid id, TaskStatut status , Guid CurrUserId);
		Task<ResponseDto> GetTaskByAssignedUserId(Guid assignedUserId);
    }
}

