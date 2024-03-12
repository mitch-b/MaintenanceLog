using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task-definitions")]
    public class TaskDefinitionController: ControllerBase
    {
        private readonly ITaskDefinitionService _taskDefinitionService;
        private readonly ITaskInstanceService _taskInstanceService;

        public TaskDefinitionController(
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService)
        {
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskDefinition>>> GetTaskDefinitions()
        {
            return Ok(await _taskDefinitionService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskDefinition>> Get(int id)
        {
            return Ok(await _taskDefinitionService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskDefinition>> AddTaskDefinition([FromBody] TaskDefinition taskDefinition)
        {
            return Ok(await _taskDefinitionService.AddAsync(taskDefinition));
        }

        [HttpPut]
        public async Task<ActionResult<TaskDefinition>> UpdateTaskDefinition([FromBody] TaskDefinition taskDefinition)
        {
            return Ok(await _taskDefinitionService.UpdateAsync(taskDefinition));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TaskDefinition>> DeleteTaskDefinition(int id)
        {
            await _taskDefinitionService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/task-instances")]
        public async Task<ActionResult<TaskInstance>> GetTaskInstances(int id)
        {
            return Ok(await _taskInstanceService.GetByTaskDefinitionAsync(id));
        }
    }
}
