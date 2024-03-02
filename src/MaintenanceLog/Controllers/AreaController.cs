using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreaController: ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IAssetService _assetService;
        public AreaController(
            IAreaService areaService,
            IAssetService assetService)
        {
            _areaService = areaService;
            _assetService = assetService;
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
    }
}
