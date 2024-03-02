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
        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
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
    }
}
