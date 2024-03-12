using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task-instances")]
    public class TaskInstanceController: ControllerBase
    {
        private readonly ITaskInstanceService _taskInstanceService;
        public TaskInstanceController(
            ITaskInstanceService taskDefinitionService)
        {
            _taskInstanceService = taskDefinitionService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskInstance>>> GetTaskInstances()
        {
            return Ok(await _taskInstanceService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskInstance>> Get(int id)
        {
            return Ok(await _taskInstanceService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskInstance>> AddTaskInstance([FromBody] TaskInstance taskDefinition)
        {
            return Ok(await _taskInstanceService.AddAsync(taskDefinition));
        }

        [HttpPut]
        public async Task<ActionResult<TaskInstance>> UpdateTaskInstance([FromBody] TaskInstance taskDefinition)
        {
            return Ok(await _taskInstanceService.UpdateAsync(taskDefinition));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TaskInstance>> DeleteTaskInstance(int id)
        {
            await _taskInstanceService.DeleteAsync(id);
            return Ok();
        }
    }
}
