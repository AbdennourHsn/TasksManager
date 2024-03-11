using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Entities;
using TaskManager.IServices;


namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _db;
        private readonly ResponseDto _response;
        private IMapper _mapper;

        public TaskService(DataContext db , IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new();
        }

        public async Task<ResponseDto> AddTask(TaskDto taskDto , Guid CurrUserId)
        {
            try
            {
                var task = _mapper.Map<TaskEntity>(taskDto);
                task.CreatorId = CurrUserId;
                _db.tasks.Add(task);
                await _db.SaveChangesAsync();

                _response.Result = task;
                return _response;
            }
            catch (Exception ex)
            {
                return ResponseDto.BadRequest($"Fail to Save task {ex}");
            }
        }

        public async Task<ResponseDto> DeleteTask(Guid id , Guid CurrUserId)
        {
            var task = await _db.tasks.FindAsync(id);
            if (task is null)
            {
                return ResponseDto.NotFounded("Task is not founded.");
            }
            if (task.CreatorId != CurrUserId && task.AssignedUserId != CurrUserId )
            {
                return ResponseDto.NotAllowed("User not allowed to delete task.");
            }

            _db.tasks.Remove(task);
            await _db.SaveChangesAsync();
            return _response;
        }

        public async Task<ResponseDto> GetAllTasks()
        {
            try
            {
                var task = await _db.tasks.ToListAsync();
                _response.Result = task;
                return _response;
            }
            catch (Exception ex)
            {
                return ResponseDto.NotFounded($"Fail to get tasks {ex}");
            }

        }

        public async Task<ResponseDto> GetSingleTask(Guid id)
        {
            var task = await _db.tasks.Include(t => t.Creator)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
            {
                return ResponseDto.NotFounded("Task is not founded.");
            }
            _response.Result = task;
            return _response;
        }

        public async Task<ResponseDto> UpdateTask(Guid id, TaskDto taskDto , Guid CurrUserId)
        {
            var task = await _db.tasks.FindAsync(id);
            if(task is null)
            {
                return ResponseDto.NotFounded("Task is not founded.");
            }
            if (task.CreatorId != CurrUserId)
            {
                return ResponseDto.NotAllowed("User not allowed to update task.");
            }
            _mapper.Map(taskDto, task);
            await _db.SaveChangesAsync();
            _response.Result = task;
            return _response;
        }

        public async Task<ResponseDto> AssignTask(Guid id, Guid TouserId , Guid CurrUserId)
        {
            var task = await _db.tasks.FindAsync(id);
            if (task is null)
            {
                return ResponseDto.NotFounded("Task is not founded.");
            }
            if (task.CreatorId != CurrUserId)
            {
                return ResponseDto.NotAllowed("User not allowed to update task.");
            }

            var user = await _db.users.FindAsync(TouserId);
            if (user is null)
            {
                return ResponseDto.NotFounded("User is not founded."); ;
            }

            task.AssignedUserId = TouserId;
            _db.tasks.Update(task);
            await _db.SaveChangesAsync();

            _response.Result = task;
            return _response; ;
        }

        public async Task<ResponseDto> UpdateTaskStatus(Guid id, TaskStatut statut , Guid CurrUserId)
        {
            var task = await _db.tasks.FindAsync(id);
            if (task is null)
            {
                return ResponseDto.NotFounded("Task is not founded.");
            }
            if(task.CreatorId!=CurrUserId && task.AssignedUserId != CurrUserId)
            {
                return ResponseDto.NotAllowed("User not allowed to update task.");
            }

            task.Statut = statut;
            _db.tasks.Update(task);
            await _db.SaveChangesAsync();

            _response.Result = task;
            return _response;
        }

        public async Task<ResponseDto> GetTaskByAssignedUserId(Guid assignedUserId)
        {
            try
            {
                var tasks = await _db.tasks
                    .Where(u => u.AssignedUserId == assignedUserId)
                    .ToListAsync();
                _response.Result = tasks;
                return _response;
            }
            catch (Exception ex)
            {
                return ResponseDto.NotFounded($"Fail to get tasks {ex}");
            }
        }


    }
}

