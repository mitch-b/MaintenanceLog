using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [ApiController]
    [Route("api/properties")]
    public class PropertyController: ControllerBase
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProperties()
        {
            return Ok(await _propertyService.GetAsync());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            return Ok(await _propertyService.FindAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddProperty([FromBody] Property property)
        {
            return Ok(await _propertyService.AddAsync(property));
        }
    }
}
