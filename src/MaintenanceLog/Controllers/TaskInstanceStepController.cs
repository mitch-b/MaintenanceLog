using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task-instance-steps")]
    public class TaskInstanceStepController : ControllerBase
    {
        private readonly ITaskInstanceService _taskInstanceService;
        private readonly ITaskInstanceStepService _taskInstanceStepService;
        public TaskInstanceStepController(
            ITaskInstanceService taskInstanceService,
            ITaskInstanceStepService taskInstanceStepService)
        {
            _taskInstanceService = taskInstanceService;
            _taskInstanceStepService = taskInstanceStepService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskInstanceStep>>> GetTaskInstanceSteps()
        {
            return Ok(await _taskInstanceStepService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskInstanceStep>> GetTaskInstanceStep(int id)
        {
            return Ok(await _taskInstanceStepService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskInstanceStep>> AddTaskInstanceStep([FromBody] TaskInstanceStep taskInstanceStep)
        {
            return Ok(await _taskInstanceStepService.AddAsync(taskInstanceStep));
        }

        [HttpPut]
        public async Task<ActionResult<TaskInstanceStep>> UpdateTaskInstanceStep([FromBody] TaskInstanceStep taskInstanceStep)
        {
            return Ok(await _taskInstanceStepService.UpdateAsync(taskInstanceStep));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TaskInstanceStep>> DeleteTaskInstanceStep(int id)
        {
            await _taskInstanceStepService.DeleteAsync(id);
            return Ok();
        }
    }
}
