﻿using MaintenanceLog.Common.Contracts;
using MaintenanceLog.Common.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/schedules")]
    public class ScheduleController: ControllerBase
    {
        private readonly ISmartScheduleService _smartScheduleService;

        public ScheduleController(
            ISmartScheduleService smartScheduleService)
        {
            _smartScheduleService = smartScheduleService;
        }

        [HttpPost]
        [Route("estimate-cron-schedule")]
        public async Task<ActionResult<string?>> EstimateCronSchedule(EstimateCronScheduleRequest requestModel)
        {
            return Ok(await _smartScheduleService.EstimateCronScheduleForItem(requestModel.ItemName, requestModel.OverridePrompts));
        }
    }
}