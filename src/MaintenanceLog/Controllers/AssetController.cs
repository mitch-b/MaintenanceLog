using MaintenanceLog.Data.Entities;
using MaintenanceLog.Data.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceLog.Controllers
{
    [ApiController]
    [Route("api/assets")]
    public class AssetController: ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
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
    }
}
