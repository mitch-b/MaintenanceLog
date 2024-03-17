using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task-definition-steps")]
    public class TaskDefinitionStepController: ControllerBase
    {
        private readonly ITaskDefinitionStepService _taskDefinitionStepService;
        private readonly ITaskDefinitionService _taskDefinitionService;
        private readonly ITaskInstanceService _taskInstanceService;
        private readonly ITaskInstanceStepService _taskInstanceStepService;
        public TaskDefinitionStepController(
            ITaskDefinitionStepService taskDefinitionStepService,
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService,
            ITaskInstanceStepService taskInstanceStepService)
        {
            _taskDefinitionStepService = taskDefinitionStepService;
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
            _taskInstanceStepService = taskInstanceStepService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskDefinitionStep>>> GetTaskDefinitionSteps()
        {
            return Ok(await _taskDefinitionStepService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskDefinitionStep>> GetTaskDefinitionStep(int id)
        {
            return Ok(await _taskDefinitionStepService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskDefinitionStep>> AddTaskDefinitionStep([FromBody] TaskDefinitionStep taskDefinitionStep)
        {
            return Ok(await _taskDefinitionStepService.AddAsync(taskDefinitionStep));
        }

        [HttpPut]
        public async Task<ActionResult<TaskDefinitionStep>> UpdateTaskDefinitionStep([FromBody] TaskDefinitionStep taskDefinitionStep)
        {
            return Ok(await _taskDefinitionStepService.UpdateAsync(taskDefinitionStep));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TaskDefinitionStep>> DeleteTaskDefinitionStep(int id)
        {
            await _taskDefinitionStepService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost]
        [Route("{taskDefinitionStepId}/task-instances/{taskInstanceId}/task-instance-step")]
        public async Task<ActionResult<TaskInstanceStep>> CreateTaskInstanceStep(int taskDefinitionStepId, int taskInstanceId)
        {
            var taskDefinitionStep = await _taskDefinitionStepService.FindAsync(taskDefinitionStepId);
            if (taskDefinitionStep is null)
            {
                return NotFound();
            }
            var taskInstance = await _taskInstanceService.FindAsync(taskInstanceId);
            if (taskInstance is null)
            {
                return NotFound();
            }
            var taskInstanceStep = new TaskInstanceStep
            {
                TaskDefinitionStepId = taskDefinitionStepId,
                TaskInstanceId = taskInstanceId,
            };
            return Ok(await _taskInstanceStepService.AddAsync(taskInstanceStep));
        }
    }
}
