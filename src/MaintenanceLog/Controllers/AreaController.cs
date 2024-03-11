using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/areas")]
    public class AreaController: ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IAssetService _assetService;
        private readonly ITaskDefinitionService _taskDefinitionService;
        public AreaController(
            IAreaService areaService,
            IAssetService assetService,
            ITaskDefinitionService taskDefinitionService)
        {
            _areaService = areaService;
            _assetService = assetService;
            _taskDefinitionService = taskDefinitionService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Area>>> GetAreas()
        {
            return Ok(await _areaService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            return Ok(await _areaService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Area>> AddArea([FromBody] Area area)
        {
            return Ok(await _areaService.AddAsync(area));
        }

        [HttpPut]
        public async Task<ActionResult<Area>> UpdateArea([FromBody] Area area)
        {
            return Ok(await _areaService.UpdateAsync(area));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Area>> DeleteArea(int id)
        {
            await _areaService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/assets")]
        public async Task<ActionResult<IList<Asset>>> GetAssets(int id)
        {
            return Ok(await _assetService.GetByAreaAsync(id));
        }

        [HttpGet]
        [Route("{id}/task-definitions")]
        public async Task<ActionResult<Asset>> GetTaskDefinitions(int id)
        {
            return Ok(await _taskDefinitionService.GetByAreaAsync(id));
        }
    }
}
