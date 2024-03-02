﻿using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [ApiController]
    [Route("api/properties")]
    public class PropertyController: ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IAreaService _areaService;
        public PropertyController(IPropertyService propertyService, IAreaService areaService)
        {
            _propertyService = propertyService;
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Property>>> GetProperties()
        {
            return Ok(await _propertyService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
            return Ok(await _propertyService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Property>> AddProperty([FromBody] Property property)
        {
            return Ok(await _propertyService.AddAsync(property));
        }

        [HttpPut]
        public async Task<ActionResult<Property>> UpdateProperty([FromBody] Property property)
        {
            return Ok(await _propertyService.UpdateAsync(property));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Property>> DeleteProperty(int id)
        {
            await _propertyService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/areas")]
        public async Task<ActionResult<IList<Area>>> GetAreas(int id)
        {
            return Ok(await _areaService.GetByPropertyAsync(id));
        }
    }
}
