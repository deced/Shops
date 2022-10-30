using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shops.Api.Base;
using Shops.Api.Base.Response;
using Shops.Api.Entities;
using Shops.Api.Models.Shop;
using Shops.Api.Models.Shop.Responses;

namespace Shops.Api.Services
{
    public interface IShopService
    {
        Task<CreateEntityResponse> Create(CreateShopModel model);
        Task<ShopUpdateOpenModelResponse> GetUpdateModel(int id);
        Task<UpdateEntityResponse> Update(UpdateShopModel model);
        Task<FilterShopResponse> Filter(string name);
        Task<DeleteEntityResponse> Delete(int id);
    }

    public class ShopService : IShopService
    {
        private readonly IBaseRepository<Shop> _shopRepository;

        public ShopService(IBaseRepository<Shop> shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<CreateEntityResponse> Create(CreateShopModel model)
        {
            var response = new CreateEntityResponse();
            var shop = new Shop()
            {
                Name = model.Name,
                Address = model.Address
            };
            await _shopRepository.CreateAsync(shop);
            response.Result = CreateEntityResponseResult.Success;
            return response;
        }

        public async Task<ShopUpdateOpenModelResponse> GetUpdateModel(int id)
        {
            var response = new ShopUpdateOpenModelResponse();
            var shop = await _shopRepository.GetByIdAsync(id);
            if (shop == null)
            {
                response.Result = ShopUpdateOpenModelResponseResult.NoSuchEntity;
                return response;
            }

            response.Id = shop.Id;
            response.Address = shop.Address;
            response.Name = shop.Name;
            response.Result = ShopUpdateOpenModelResponseResult.Success;
            return response;
        }

        public async Task<UpdateEntityResponse> Update(UpdateShopModel model)
        {
            var response = new UpdateEntityResponse();
            var shop = await _shopRepository.GetByIdAsync(model.Id);
            if (shop == null)
            {
                response.Result = UpdateEntityRepositoryResult.NoSuchEntity;
                return response;
            }

            shop.Address = model.Address;
            shop.Name = model.Name;
            await _shopRepository.UpdateAsync(shop);
            response.Result = UpdateEntityRepositoryResult.Success;
            return response;
        }

        public async Task<FilterShopResponse> Filter(string name)
        {
            var response = new FilterShopResponse();
            name ??= "";
            var shops = await _shopRepository.FilterWithIncludeAsync(
                x => x.Name.ToLower().Contains(name.ToLower()),
                x => x.Orders,
                x => x.Products);

            response.Items = shops.Select(x => new FilterShopResponseItem()
            {
                Address = x.Address,
                Id = x.Id,
                Name = x.Name,
                OrdersCount = x.Orders.Count,
                ProductsCount = x.Products.Count
            });
            response.Result = FilterShopResponseResult.Success;
            return response;
        }

        public async Task<DeleteEntityResponse> Delete(int id)
        {
            var response = new DeleteEntityResponse();
            var shop = await _shopRepository.GetByIdWithIncludeAsync(id, 
                x => x.Products,
                x => x.Orders);
            if (shop == null)
            {
                response.Result = DeleteEntityResponseResult.NoSuchEntity;
                return response;
            }

            await _shopRepository.DeleteAsync(shop);
            response.Result = DeleteEntityResponseResult.Success;
            return response;
        }
    }
}