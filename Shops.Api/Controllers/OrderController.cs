using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shops.Api.Base.Response;
using Shops.Api.Models.Order;
using Shops.Api.Models.Order.Responses;
using Shops.Api.Models.Products;
using Shops.Api.Models.Products.Responses;
using Shops.Api.Services;

namespace Shops.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<FilterOrderResponse> Filter([FromQuery]string shopName)
        {
            return await _orderService.Filter(shopName);
        }

        [HttpGet("{shopId}")]
        public async Task<OrderCreateOpenModelResponse> GetCreateModel(int shopId)
        {
           return await _orderService.GetCreateModel(shopId);
        }

        [HttpPost]
        public async Task<CreateEntityResponse> Create([FromBody] CreateOrderModel model)
        {
            return await _orderService.Create(model);
        }
        
        [HttpPut]
        public async Task<ConfirmEntityResponse> Confirm([FromQuery] int id)
        {
           return await _orderService.Confirm(id);
        }
        
        [HttpPut]
        public async Task<DeclineEntityResponse> Decline([FromQuery] int id)
        {
            return await _orderService.Decline(id);
        }
    }
}