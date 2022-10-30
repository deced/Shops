using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shops.App.Base.Handlers;
using Shops.App.Base.Models;
using Shops.App.Handlers.Product;
using Shops.App.Models.Product;

namespace Shops.App.Controllers
{
    [Route("[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly ICreateEntityHandler<CreateProductModel> _createEntityHandler;
        private readonly IUpdateEntityHandler<UpdateProductModel> _updateEntityHandler;
        private readonly IGetProductCreateModelHandler _getProductCreateModelHandler;
        private readonly IGetProductUpdateModelHandler _getProductUpdateModelHandler;
        private readonly IFilterProductHandler _filterProductHandler;
        private readonly IDeleteEntityHandler _deleteEntityHandler;

        public ProductController(ICreateEntityHandler<CreateProductModel> createEntityHandler,
            IUpdateEntityHandler<UpdateProductModel> updateEntityHandler,
            IGetProductCreateModelHandler getProductCreateModelHandler,
            IGetProductUpdateModelHandler getProductUpdateModelHandler,
            IFilterProductHandler filterProductHandler,
            IDeleteEntityHandler deleteEntityHandler)
        {
            _createEntityHandler = createEntityHandler;
            _updateEntityHandler = updateEntityHandler;
            _getProductCreateModelHandler = getProductCreateModelHandler;
            _getProductUpdateModelHandler = getProductUpdateModelHandler;
            _filterProductHandler = filterProductHandler;
            _deleteEntityHandler = deleteEntityHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<FilterProductHandlerResponse> Filter([FromQuery] string name)
        {
            return await _filterProductHandler.ExecuteAsync(name);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateProductOpenModel();
            var response = await _getProductCreateModelHandler.ExecuteAsync();
            viewModel.Shops = response.Shops;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProductModel model)
        {
            return Ok(await _createEntityHandler.ExecuteAsync("Product", model));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _getProductUpdateModelHandler.ExecuteAsync(id);
            var viewModel = new UpdateProductOpenModel()
            {
                Color = response.Color,
                Id = response.Id,
                Name = response.Name,
                Price = response.Price.ToString(CultureInfo.CreateSpecificCulture("en-GB")),
                ShopName = response.ShopName
            };
            return View(viewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateProductModel model)
        {
            return Ok(await _updateEntityHandler.ExecuteAsync("Product", model));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            return Ok(await _deleteEntityHandler.ExecuteAsync("Product", id));
        }
    }
}