using System.Linq;
using System.Threading.Tasks;
using Shops.Api.Base;
using Shops.Api.Base.Response;
using Shops.Api.Entities;
using Shops.Api.Models.Products;
using Shops.Api.Models.Products.Responses;

namespace Shops.Api.Services
{
    public interface IProductService
    {
        Task<CreateEntityResponse> Create(CreateProductModel model);
        Task<UpdateEntityResponse> Update(UpdateProductModel model);
        Task<ProductCreateOpenModelResponse> GetCreateModel();
        Task<ProductUpdateOpenModelResponse> GetUpdateModel(int id);
        Task<FilterProductResponse> Filter(string name);
        Task<DeleteEntityResponse> Delete(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Shop> _shopRepository;

        public ProductService(IBaseRepository<Product> productRepository,
            IBaseRepository<Shop> shopRepository)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
        }

        public async Task<CreateEntityResponse> Create(CreateProductModel model)
        {
            var response = new CreateEntityResponse();
            var shop = await _shopRepository.GetByIdAsync(model.ShopId);
            if (shop == null)
            {
                response.Result = CreateEntityResponseResult.Error;
                return response;
            }
            
            var product = new Product()
            {
                Name = model.Name,
                Color = model.Color,
                Price = model.Price,
                ShopId = model.ShopId
            };
            await _productRepository.CreateAsync(product);
            response.Result = CreateEntityResponseResult.Success;
            return response;
        }

        public async Task<UpdateEntityResponse> Update(UpdateProductModel model)
        {
            var response = new UpdateEntityResponse();
            var product = await _productRepository.GetByIdAsync(model.Id);
            if (product == null)
            {
                response.Result = UpdateEntityRepositoryResult.NoSuchEntity;
                return response;
            }

            product.Color = model.Color;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productRepository.UpdateAsync(product);
            response.Result = UpdateEntityRepositoryResult.Success;
            return response;
        }

        public async Task<ProductCreateOpenModelResponse> GetCreateModel()
        {
            var response = new ProductCreateOpenModelResponse();
            var shops = await _shopRepository.GetAllAsync();
            response.Shops = shops.Select(x => new ShopDropdownModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            response.Result = ProductCreateOpenModelResponseResult.Success;
            return response;
        }

        public async Task<ProductUpdateOpenModelResponse> GetUpdateModel(int id)
        {
            var response = new ProductUpdateOpenModelResponse();
            var product = await _productRepository.GetByIdWithIncludeAsync(id,
                x => x.Shop);
            response.Color = product.Color;
            response.Id = product.Id;
            response.Name = product.Name;
            response.Price = product.Price;
            response.ShopName = product.Shop.Name;
            response.Result = ProductUpdateOpenModelResponseResult.Success;
            return response;
        }

        public async Task<FilterProductResponse> Filter(string name)
        {
            var response = new FilterProductResponse();
            name ??= "";
            var products = await _productRepository.FilterWithIncludeAsync(
                x => x.Name.ToLower().Contains(name.ToLower()),
                x => x.Shop);

            response.Items = products.Select(x => new FilterProductResponseItem()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
                Price = x.Price,
                ShopName = x.Shop.Name
            });
            response.Result = FilterProductResponseResult.Success;
            return response;
        }

        public async Task<DeleteEntityResponse> Delete(int id)
        {
            var response = new DeleteEntityResponse();
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                response.Result = DeleteEntityResponseResult.NoSuchEntity;
                return response;
            }

            await _productRepository.DeleteAsync(product);
            response.Result = DeleteEntityResponseResult.Success;
            return response;
        }
    }
}