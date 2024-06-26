﻿using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task-types")]
    public class TaskTypeController: ControllerBase
    {
        private readonly ITaskTypeService _taskTypeService;
        private readonly ITaskDefinitionService _taskDefinitionService;
        private readonly ITaskInstanceService _taskInstanceService;

        public TaskTypeController(
            ITaskTypeService taskTypeService,
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService)
        {
            _taskTypeService = taskTypeService;
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
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

        [HttpGet]
        [Route("{id}/task-definitions")]
        public async Task<ActionResult<Asset>> GetTaskDefinitions(int id)
        {
            return Ok(await _taskDefinitionService.GetByTaskTypeAsync(id));
        }

        [HttpGet]
        [Route("{id}/task-instances")]
        public async Task<ActionResult<TaskInstance>> GetTaskInstances(int id)
        {
            return Ok(await _taskInstanceService.GetByTaskTypeAsync(id));
        }
    }
}
