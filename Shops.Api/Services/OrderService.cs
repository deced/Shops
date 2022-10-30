using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shops.Api.Base;
using Shops.Api.Base.Response;
using Shops.Api.Entities;
using Shops.Api.Models.Order;
using Shops.Api.Models.Order.Responses;

namespace Shops.Api.Services
{
    public interface IOrderService
    {
        Task<CreateEntityResponse> Create(CreateOrderModel model);
        Task<OrderCreateOpenModelResponse> GetCreateModel(int shopId);
        Task<ConfirmEntityResponse> Confirm(int id);
        Task<DeclineEntityResponse> Decline(int id);
        Task<FilterOrderResponse> Filter(string shopName);
        Task<DeleteEntityResponse> Delete(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Shop> _shopRepository;
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<OrderedProduct> _orderedProductRepository;

        public OrderService(IBaseRepository<Shop> shopRepository,
            IBaseRepository<Order> orderRepository,
            IBaseRepository<Product> productRepository,
            IBaseRepository<OrderedProduct> orderedProductRepository)
        {
            _shopRepository = shopRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderedProductRepository = orderedProductRepository;
        }

        private async Task CreateLinkedOrderedProducts(IEnumerable<int> ids, int orderId)
        {
            var products = await _productRepository.FilterAsync(x => ids.Contains(x.Id));
            var orderedProducts = products.Select(x => new OrderedProduct()
            {
                Color = x.Color,
                Name = x.Name,
                Price = x.Price,
                OrderId = orderId
            });
            await _orderedProductRepository.CreateManyAsync(orderedProducts);
        }

        public async Task<CreateEntityResponse> Create(CreateOrderModel model)
        {
            var response = new CreateEntityResponse();
            var order = new Order()
            {
                ShopId = model.ShopId,
                Status = OrderStatus.Waiting,
                ShippingAddress = model.ShippingAddress
            };
            var orderId = await _orderRepository.CreateAsync(order);
            await CreateLinkedOrderedProducts(model.ProductIds, orderId);
            response.Result = CreateEntityResponseResult.Success;
            return response;
        }

        public async Task<OrderCreateOpenModelResponse> GetCreateModel(int shopId)
        {
            var response = new OrderCreateOpenModelResponse();
            var shop = await _shopRepository.GetByIdWithIncludeAsync(shopId,
                x => x.Products);
            if (shop == null)
            {
                response.Result = OrderCreateOpenModelResponseResult.NoSuchShop;
                return response;
            }

            response.ShopId = shop.Id;
            response.ShopName = shop.Name;
            response.Products = shop.Products.Select(x => new OrderCreateProductModel()
            {
                Color = x.Color,
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });
            response.Result = OrderCreateOpenModelResponseResult.Success;
            return response;
        }

        public async Task<ConfirmEntityResponse> Confirm(int id)
        {
            var response = new ConfirmEntityResponse();
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                response.Result = ConfirmEntityResponseResult.NoSuchEntity;
                return response;
            }

            order.Status = OrderStatus.Completed;
            await _orderRepository.UpdateAsync(order);
            return response;
        }

        public async Task<DeclineEntityResponse> Decline(int id)
        {  
            var response = new DeclineEntityResponse();
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                response.Result = DeclineEntityResponseResult.NoSuchEntity;
                return response;
            }

            order.Status = OrderStatus.Declined;
            await _orderRepository.UpdateAsync(order);
            return response;
        }

        public async Task<FilterOrderResponse> Filter(string shopName)
        {
            var response = new FilterOrderResponse();
            shopName ??= "";
            var orders = await _orderRepository.FilterWithIncludeAsync(
                x => x.Shop.Name.ToLower().Contains(shopName.ToLower()),
                x => x.Products,
                x => x.Shop);

            response.Items = orders.Select(x => new FilterOrderResponseItem()
            {
                Id = x.Id,
                Status = x.Status,
                ProductsCount = x.Products.Count(),
                ShopName = x.Shop.Name,
                TotalPrice = x.Products.Sum(x => x.Price)
            });
            response.Items = response.Items.OrderBy(x => x.Status);
            response.Result = FilterOrderResponseResult.Success;
            return response;
        }

        public async Task<DeleteEntityResponse> Delete(int id)
        {
            var response = new DeleteEntityResponse();
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                response.Result = DeleteEntityResponseResult.NoSuchEntity;
                return response;
            }

            await _orderRepository.DeleteAsync(order);
            response.Result = DeleteEntityResponseResult.Success;
            return response;
        }
    }
}