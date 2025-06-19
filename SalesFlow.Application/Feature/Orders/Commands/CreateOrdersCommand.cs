
using AutoMapper;
using MediatR;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;

namespace SalesFlow.Application.Feature.Orders.Commands
{
    public class CreateOrdersCommand : IRequest<ApiResponse<string>>
    {
        public int IdCustomer { get; set; }
        public int IdEmploye { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public OrderStatus StatusOrder { get; set; }
        public string OrderType { get; set; }

        // Agregar una lista de detalles de la orden
        public List<CreateOrderDetailCommandDto> OrderDetails { get; set; } = new List<CreateOrderDetailCommandDto>();
    }

    public class CreateOrderDetailCommandDto
    {
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }


    public class CreateOrderCommandHandler : IRequestHandler<CreateOrdersCommand, ApiResponse<string>>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(
            IOrderRepository repository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IRecipeRepository recipeRepository,
            IMapper mapper)
        {
            _repository = repository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
        {
            var newOrder = new Order();
            newOrder.StatusOrder = command.StatusOrder;
            newOrder.DateOrder = DateTime.Now;
            newOrder.IdCustomer = command.IdCustomer;
            newOrder.IdEmploye = command.IdEmploye;
            newOrder.OrderType = command.OrderType;
            newOrder.Total = 0; // Inicializamos el total

            await _repository.InsertAndSave(newOrder);

            decimal totalOrder = 0;

            foreach (var detail in command.OrderDetails)
            {
                var product = await _productRepository.Get(x => x.Id == detail.IdProduct);
                if (product == null)
                    return new ApiResponse<string>()
                    {
                        Message = "Producto no encontrado.",
                        Succeeded = false
                    };

                var unitPrice = product.Price;
                var subtotal = unitPrice * detail.Amount;

                var newDetail = new OrderDetail
                {
                    IdOrder = newOrder.Id,
                    IdProduct = detail.IdProduct,
                    Amount = detail.Amount,
                    UnitPrice = unitPrice,
                    SubTotal = subtotal
                };

                totalOrder += subtotal;
                await _orderDetailRepository.InsertAndSave(newDetail);

                // Lógica para actualizar inventario según tipo de producto
                if (product.ProductType == ProductTypeEnum.Composed)
                {
                    // Producto compuesto, verificar ingredientes
                    var recipes = await _recipeRepository.GetAll(r => r.IdProduct == product.Id);
                    foreach (var recipe in recipes)
                    {
                        var ingredientInventory = await _inventoryRepository.Get(i => i.IdProduct == recipe.IdIngredient);
                        if (ingredientInventory == null)
                        {
                            return new ApiResponse<string>
                            {
                                Message = $"No hay inventario para el ingrediente requerido del producto {product.Name}.",
                                Succeeded = false
                            };
                        }

                        var requiredAmount = recipe.Amount * detail.Amount;

                        if (ingredientInventory.AvailableQuantity < requiredAmount)
                        {
                            return new ApiResponse<string>
                            {
                                Message = $"Inventario insuficiente para el ingrediente {ingredientInventory.Product.Name}.",
                                Succeeded = false
                            };
                        }

                        ingredientInventory.AvailableQuantity -= requiredAmount;
                        ingredientInventory.DateUpdate = DateTime.UtcNow;
                        await _inventoryRepository.UpdateAndSave(ingredientInventory);
                    }
                }
                else
                {
                    // Producto simple, descontar directamente del inventario
                    var inventory = await _inventoryRepository.Get(i => i.IdProduct == detail.IdProduct);
                    if (inventory == null)
                        return new ApiResponse<string>()
                        {
                            Message = $"No hay inventario registrado para el producto {product.Name}",
                            Succeeded = false
                        };

                    if (inventory.AvailableQuantity < detail.Amount)
                        return new ApiResponse<string>()
                        {
                            Message = $"Inventario insuficiente para el producto {product.Name}",
                            Succeeded = false
                        };

                    inventory.AvailableQuantity -= detail.Amount;
                    inventory.DateUpdate = DateTime.UtcNow;
                    await _inventoryRepository.UpdateAndSave(inventory);
                }
            }

            newOrder.Total = totalOrder;
            await _repository.UpdateAndSave(newOrder); // Actualiza el total en la orden

            return new ApiResponse<string>("Orden registrada correctamente");
        }
    }

}
