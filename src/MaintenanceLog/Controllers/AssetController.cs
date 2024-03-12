using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/assets")]
    public class AssetController: ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly ITaskDefinitionService _taskDefinitionService;
        private readonly ITaskInstanceService _taskInstanceService;
        public AssetController(IAssetService assetService, 
            ITaskDefinitionService taskDefinitionService,
            ITaskInstanceService taskInstanceService)
        {
            _assetService = assetService;
            _taskDefinitionService = taskDefinitionService;
            _taskInstanceService = taskInstanceService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Asset>>> GetAssets()
        {
            return Ok(await _assetService.GetAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            return Ok(await _assetService.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> AddAsset([FromBody] Asset asset)
        {
            return Ok(await _assetService.AddAsync(asset));
        }

        [HttpPut]
        public async Task<ActionResult<Asset>> UpdateAsset([FromBody] Asset asset)
        {
            return Ok(await _assetService.UpdateAsync(asset));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Asset>> DeleteAsset(int id)
        {
            await _assetService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/task-definitions")]
        public async Task<ActionResult<Asset>> GetTaskDefinitions(int id)
        {
            return Ok(await _taskDefinitionService.GetByAssetAsync(id));
        }

        [HttpGet]
        [Route("{id}/task-instances")]
        public async Task<ActionResult<TaskInstance>> GetTaskInstances(int id)
        {
            return Ok(await _taskInstanceService.GetByAssetAsync(id));
        }
    }
}
