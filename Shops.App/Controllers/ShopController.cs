using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shops.App.Base.Handlers;
using Shops.App.Handlers.Shop;
using Shops.App.Models.Shop;

namespace Shops.App.Controllers
{
    [Route("[controller]/[action]")]
    public class ShopController : Controller
    {
        private readonly ICreateEntityHandler<CreateShopModel> _createEntityHandler;
        private readonly IUpdateEntityHandler<UpdateShopModel> _updateEntityHandler;
        private readonly IGetShopUpdateModelHandler _getShopUpdateModelHandler;
        private readonly IFilterShopHandler _filterShopHandler;
        private readonly IDeleteEntityHandler _deleteEntityHandler;

        public ShopController(ICreateEntityHandler<CreateShopModel> createEntityHandler,
            IUpdateEntityHandler<UpdateShopModel> updateEntityHandler,
            IGetShopUpdateModelHandler getShopUpdateModelHandler,
            IFilterShopHandler filterShopHandler,
            IDeleteEntityHandler deleteEntityHandler)
        {
            _createEntityHandler = createEntityHandler;
            _updateEntityHandler = updateEntityHandler;
            _getShopUpdateModelHandler = getShopUpdateModelHandler;
            _filterShopHandler = filterShopHandler;
            _deleteEntityHandler = deleteEntityHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<FilterShopHandlerResponse> Filter([FromQuery] string name)
        {
            return await _filterShopHandler.ExecuteAsync(name);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateShopModel model)
        {
            return Ok(await _createEntityHandler.ExecuteAsync("Shop", model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _getShopUpdateModelHandler.ExecuteAsync(id);
            var viewModel = new UpdateShopModel()
            {
                Address = response.Address,
                Id = response.Id,
                Name = response.Name
            };
            return View(viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateShopModel model)
        {
            return Ok(await _updateEntityHandler.ExecuteAsync("Shop", model));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return Ok(await _deleteEntityHandler.ExecuteAsync("Shop", id));
        }
    }
}