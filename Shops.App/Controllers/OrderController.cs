using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shops.App.Base.Handlers;
using Shops.App.Handlers.Order;
using Shops.App.Models.Order;

namespace Shops.App.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly ICreateEntityHandler<CreateOrderModel> _createEntityHandler;
        private readonly IConfirmEntityHandler _confirmOrderHandler;
        private readonly IDeclineEntityHandler _declineOrderHandler;
        private readonly IGetOrderCreateModelHandler _getOrderCreateModelHandler;
        private readonly IFilterOrderHandler _filterOrderHandler;

        public OrderController(ICreateEntityHandler<CreateOrderModel> createEntityHandler,
            IGetOrderCreateModelHandler getOrderCreateModelHandler,
            IDeclineEntityHandler declineOrderHandler,
            IConfirmEntityHandler confirmOrderHandler,
            IFilterOrderHandler filterOrderHandler)
        {
            _createEntityHandler = createEntityHandler;
            _confirmOrderHandler = confirmOrderHandler;
            _declineOrderHandler = declineOrderHandler;
            _getOrderCreateModelHandler = getOrderCreateModelHandler;
            _filterOrderHandler = filterOrderHandler;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<FilterOrderHandlerResponse> Filter([FromQuery] string shopName)
        {
            return await _filterOrderHandler.ExecuteAsync(shopName);
        }
        
        [HttpGet("{shopId}")]
        [Authorize(Roles = "adimin,user")]
        public async Task<IActionResult> Create(int shopId)
        {
            var viewModel = new CreateOrderOpenModel();
            var response = await _getOrderCreateModelHandler.ExecuteAsync(shopId);
            viewModel.Products = response.Products;
            viewModel.ShopId = response.ShopId;
            viewModel.ShopName = response.ShopName;
            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize(Roles = "adimin,user")]
        public async Task<IActionResult> Create([FromBody]CreateOrderModel model)
        {
            return Ok(await _createEntityHandler.ExecuteAsync("Order", model));
        }
        
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ConfirmEntityHandlerResponse> Confirm([FromQuery] int id)
        {
            return await _confirmOrderHandler.ExecuteAsync("Order",id);
        }
        
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<DeclineEntityHandlerResponse> Decline([FromQuery] int id)
        {
            return await _declineOrderHandler.ExecuteAsync("Order",id);
        }
    }
}