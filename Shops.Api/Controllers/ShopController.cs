using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shops.Api.Base.Response;
using Shops.Api.Models.Shop;
using Shops.Api.Models.Shop.Responses;
using Shops.Api.Services;

namespace Shops.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<FilterShopResponse> Filter([FromQuery]string name)
        {
            return await _shopService.Filter(name);
        }

        [HttpPost]
        public async Task<CreateEntityResponse> Create([FromBody]CreateShopModel model)
        {
            return await _shopService.Create(model);
        }

        [HttpGet("{id}")]
        public async Task<ShopUpdateOpenModelResponse> GetUpdateModel(int id)
        {
            return await _shopService.GetUpdateModel(id);
        }

        [HttpPut]
        public async Task<UpdateEntityResponse> Update([FromBody] UpdateShopModel model)
        {
            return await _shopService.Update(model);
        }

        [HttpDelete]
        public async Task<DeleteEntityResponse> Delete([FromQuery] int id)
        {
            return await _shopService.Delete(id);
        }
    }
}