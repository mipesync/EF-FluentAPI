using EF_FluentAPI.BLL.Interfaces;

namespace EF_FluentAPI.BLL
{
    public class ServiceManager
    {
        private IAuthService _authService;
        private ICartService _cartService;
        private ICustomerService _customerService;
        private ICredentialService _credentialService;
        private IProductService _productService;
        private IOrderService _orderService;

        public ServiceManager(IAuthService authService, ICartService cartService, ICustomerService customerService,
            ICredentialService credentialService, IProductService productService, IOrderService orderService)
        {
            _authService = authService;
            _cartService = cartService;
            _customerService = customerService;
            _credentialService = credentialService;
            _productService = productService;
            _orderService = orderService;
        }

        public IAuthService AuthService { get { return _authService; } }
        public ICartService CartService { get { return _cartService; } }
        public ICustomerService CustomerService { get { return _customerService; } }
        public ICredentialService CredentialService { get { return _credentialService; } }
        public IProductService ProductService { get { return _productService; } }
        public IOrderService OrderService { get { return _orderService; } }
    }
}
