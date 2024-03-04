using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [ApiController]
    [Route("api/task-types")]
    public class TaskTypeController: ControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        public TaskTypeController(
            ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskType>>> GetTaskTypes()
        {
            return Ok(await _taskTypeService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskType>> GetTaskType(int id)
        {
            return Ok(await _taskTypeService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskType>> AddTaskType([FromBody] TaskType taskType)
        {
            return Ok(await _taskTypeService.AddAsync(taskType));
        }

        [HttpPut]
        public async Task<ActionResult<TaskType>> UpdateTaskType([FromBody] TaskType taskType)
        {
            return Ok(await _taskTypeService.UpdateAsync(taskType));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TaskType>> DeleteTaskType(int id)
        {
            await _taskTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}
