using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/properties")]
    public class PropertyController: ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IAreaService _areaService;
        private readonly IAssetService _assetService;
        private readonly ITaskDefinitionService _taskDefinitionService;
        private readonly ITaskInstanceService _taskInstanceService;
        public PropertyController(
            IPropertyService propertyService, 
            IAreaService areaService,
            IAssetService assetService,
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService)
        {
            _propertyService = propertyService;
            _areaService = areaService;
            _assetService = assetService;
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
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

        [HttpGet]
        [Route("{id}/assets")]
        public async Task<ActionResult<IList<Asset>>> GetAssets(int id)
        {
            return Ok(await _assetService.GetByPropertyAsync(id));
        }

        [HttpGet]
        [Route("{id}/task-definitions")]
        public async Task<ActionResult<Asset>> GetTaskDefinitions(int id)
        {
            return Ok(await _taskDefinitionService.GetByPropertyAsync(id));
        }

        [HttpGet]
        [Route("{id}/task-instances")]
        public async Task<ActionResult<TaskInstance>> GetTaskInstances(int id)
        {
            return Ok(await _taskInstanceService.GetByPropertyAsync(id));
        }
    }
}
