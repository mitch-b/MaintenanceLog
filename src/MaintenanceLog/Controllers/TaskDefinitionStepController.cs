using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Requests;
using MaintenanceLog.Common.Models.Responses;
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
        private readonly ISmartTaskDefinitionStepService _smartTaskDefinitionStepService;
        public TaskDefinitionStepController(
            ITaskDefinitionStepService taskDefinitionStepService,
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService,
            ITaskInstanceStepService taskInstanceStepService,
            ISmartTaskDefinitionStepService smartTaskDefinitionStepService)
        {
            _taskDefinitionStepService = taskDefinitionStepService;
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
            _taskInstanceStepService = taskInstanceStepService;
            _smartTaskDefinitionStepService = smartTaskDefinitionStepService;
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

        [HttpPost]
        [Route("suggest")]
        public async Task<ActionResult<List<string>>> EstimateTaskDefinitionSteps([FromBody] SuggestTaskDefinitionStepsRequest request)
        {
            var suggestedSteps = await _smartTaskDefinitionStepService.SuggestStepsForTaskDefinition(request.Name, request.Description, request.Steps, request.OverrideSystemPrompts);
            if (suggestedSteps is null)
            {
                return BadRequest("Unable to suggest any steps.");
            }
            return Ok(new SuggestTaskDefinitionStepsResponse { Steps = suggestedSteps });
        }
    }
}
