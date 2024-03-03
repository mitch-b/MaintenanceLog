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
            ITaskTypeService areaService)
        {
            _taskTypeService = areaService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskType>>> GetAreas()
        {
            return Ok(await _taskTypeService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskType>> GetArea(int id)
        {
            return Ok(await _taskTypeService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TaskType>> AddArea([FromBody] TaskType area)
        {
            return Ok(await _taskTypeService.AddAsync(area));
        }

        [HttpPut]
        public async Task<ActionResult<TaskType>> UpdateArea([FromBody] TaskType area)
        {
            return Ok(await _taskTypeService.UpdateAsync(area));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TaskType>> DeleteArea(int id)
        {
            await _taskTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}
