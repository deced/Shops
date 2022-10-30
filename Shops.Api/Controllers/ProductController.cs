using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shops.Api.Base.Response;
using Shops.Api.Models.Products;
using Shops.Api.Models.Products.Responses;
using Shops.Api.Services;

namespace Shops.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<FilterProductResponse> Filter([FromQuery]string name)
        {
            return await _productService.Filter(name);
        }

        [HttpGet]
        public async Task<ProductCreateOpenModelResponse> GetCreateModel()
        {
            return await _productService.GetCreateModel();
        }

        [HttpPost]
        public async Task<CreateEntityResponse> Create([FromBody] CreateProductModel model)
        {
            return await _productService.Create(model);
        }

        [HttpGet("{id}")]
        public async Task<ProductUpdateOpenModelResponse> GetUpdateModel(int id)
        {
            return await _productService.GetUpdateModel(id);
        }

        [HttpPut]
        public async Task<UpdateEntityResponse> Update([FromBody] UpdateProductModel model)
        {
            return await _productService.Update(model);
        }

        [HttpDelete]
        public async Task<DeleteEntityResponse> Delete([FromQuery] int id)
        {
            return await _productService.Delete(id);
        }
    }
}