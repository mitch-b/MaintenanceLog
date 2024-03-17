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
        private readonly ITaskDefinitionStepService _taskDefinitionStepService;
        private readonly ITaskInstanceStepService _taskInstanceStepService;

        public TaskDefinitionController(
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService,
            ITaskDefinitionStepService taskDefinitionStepService,
            ITaskInstanceStepService taskInstanceStepService)
        {
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
            _taskDefinitionStepService = taskDefinitionStepService;
            _taskInstanceStepService = taskInstanceStepService;
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

        [HttpGet]
        [Route("{id}/task-definition-steps")]
        public async Task<ActionResult<TaskInstance>> GetTaskDefinitionSteps(int id)
        {
            return Ok(await _taskDefinitionStepService.GetByTaskDefinitionAsync(id));
        }

        [HttpPost]
        [Route("{taskDefinitionId}/task-instances/{taskInstanceId}/task-instance-steps")]
        public async Task<ActionResult<TaskDefinition>> AddTaskInstanceStepsFromTaskDefinitionSteps(int taskDefinitionId, int taskInstanceId)
        {
            return Ok(await _taskInstanceStepService.CreateFromTaskDefinitionAsync(taskInstanceId, taskDefinitionId));
        }
    }
}
