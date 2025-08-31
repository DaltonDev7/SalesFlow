using AutoMapper;
using MediatR;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;
using System.Net;

namespace SalesFlow.Application.Feature.Orders.Commands
{
    // ===== Servicio de Alertas (colocado aquí mismo para tu prueba) =====
    public interface IAlertService
    {
        Task SendLowStockAsync(string productName, int remaining, CancellationToken ct = default);
    }

    public class AlertService : IAlertService
    {
        public Task SendLowStockAsync(string productName, int remaining, CancellationToken ct = default)
        {
            // Ajusta el destino real (Slack/Email/Log)
            // Nota: Mantengo el formato que pediste "Queda X ... disponibles".

           
            Console.WriteLine($"Queda {remaining} {productName} disponibles en el inventario.");
            return Task.CompletedTask;
        }
    }
    // ====================================================================

    public class CreateOrdersCommand : IRequest<ApiResponse<List<ProductoFaltanteConteoDto>>>
    {
        public int? IdCustomer { get; set; }
        public string? CustomerName { get; set; }
        public int IdEmploye { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Total { get; set; }
        public OrderStatus StatusOrder { get; set; }

        public int? IdPaymentMethod { get; set; }
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



    public class ProductoFaltanteConteoDto 
    {
        public string? ProductName { get; set; }
        public int CantidadDisponible { get; set; }

    }

    public interface IProductosFaltanteManager
    {
        List<ProductoFaltanteConteoDto> GetData();
        void SetData(string name, int cantidad);
    }

    public class ProductosFaltanteManager : IProductosFaltanteManager
    {
        private List<ProductoFaltanteConteoDto> productos = new List<ProductoFaltanteConteoDto>();

        public void SetData(string name, int cantidad)
        {
            productos.Add(new ProductoFaltanteConteoDto { ProductName = name, CantidadDisponible = cantidad });
        }

        public List<ProductoFaltanteConteoDto> GetData()
        {
            return productos;
        }
    }



    public interface IProductosFaltantesConteo
    {
        public void SetData();
        public string GetData();    
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrdersCommand, ApiResponse<List<ProductoFaltanteConteoDto>>>
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly IAlertService _alertService;
        private List<ProductoFaltanteConteoDto> productosFaltantesConteo = new List<ProductoFaltanteConteoDto>() { };
        private const int LowStockThresholdExclusive = 20; // "menos de 20"

        public CreateOrderCommandHandler(
            IOrderRepository repository,
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IRecipeRepository recipeRepository,
            IMapper mapper,
            IAlertService alertService // inyectado
        )
        {
            _repository = repository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _alertService = alertService;
        }

        public async Task<ApiResponse<List<ProductoFaltanteConteoDto>>> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
        {
            var newOrder = new Order
            {
                StatusOrder = command.StatusOrder,
                DateOrder = DateTime.Now,
                IdCustomer = command.IdCustomer,
                IdPaymentMethod = command.IdPaymentMethod,
                CustomerName = command.CustomerName,
                IdEmploye = command.IdEmploye,
                OrderType = command.OrderType,
                Total = 0 // Inicializamos el total
            };

            try
            {
                await _repository.InsertAndSave(newOrder);
            }
            catch (System.Exception error)
            {
                Console.WriteLine(error);
            }

            decimal totalOrder = 0;

            foreach (var detail in command.OrderDetails)
            {
                var product = await _productRepository.Get(x => x.Id == detail.IdProduct);
                if (product == null)
                    return new ApiResponse<List<ProductoFaltanteConteoDto>> ()
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
                    // Producto compuesto, verificar y descontar ingredientes
                    var recipes = await _recipeRepository.GetAll(r => r.IdProduct == product.Id);
                    foreach (var recipe in recipes)
                    {
                        var requiredAmount = (int)(recipe.Amount * detail.Amount);

                        var ingredientInventory = await _inventoryRepository.Get(i => i.IdProduct == recipe.IdIngredient);
                        var ingredientProduct = await _productRepository.Get(p => p.Id == recipe.IdIngredient);
                        var ingredientName = ingredientProduct?.Name ?? $"Ingrediente {recipe.IdIngredient}";

                        try
                        {
                            await ValidateDecrementAndNotifyAsync(
                                ingredientInventory,
                                requiredAmount,
                                ingredientName,
                                cancellationToken);

                        }
                        catch
                        {
                            // Si algo falla al descontar un ingrediente, revertimos la orden creada
                            await _repository.DeleteAndSave(newOrder.Id);
                            await _orderDetailRepository.DeleteAndSave(newDetail.Id);
                           // throw;
                        }
                    }
                }
                else
                {
                    // Producto simple: validar → descontar → notificar
                    var inventory = await _inventoryRepository.Get(i => i.IdProduct == detail.IdProduct);

                    try
                    {
                         await ValidateDecrementAndNotifyAsync(
                            inventory,
                            detail.Amount,
                            product.Name,
                            cancellationToken);
                    }
                    catch
                    {
                        await _repository.DeleteAndSave(newOrder.Id);
                        await _orderDetailRepository.DeleteAndSave(newDetail.Id);
                        throw;
                    }
                }
            }

            newOrder.Total = totalOrder;
            await _repository.UpdateAndSave(newOrder); // Actualiza el total en la orden

            return new ApiResponse<List<ProductoFaltanteConteoDto>>()
            {
                Data = productosFaltantesConteo,
                Message = "Orden Registrada",
                Succeeded = true,
            };
        }

        // ===== Helper: validar stock → descontar → notificar si queda < 20 =====
        private async Task ValidateDecrementAndNotifyAsync(
            Inventory inventory,
            int decrementAmount,
            string productName,
            CancellationToken ct)
        {
            // 1) Validar existencia de inventario
            if (inventory == null)
                throw new ApiException($"No hay inventario registrado para el producto {productName}", (int)HttpStatusCode.InternalServerError);

            // 2) Validar suficiente stock
            if (inventory.AvailableQuantity < decrementAmount)
                throw new ApiException($"Inventario insuficiente para el producto {productName}", (int)HttpStatusCode.InternalServerError);

            // 3) Descontar
            inventory.AvailableQuantity -= decrementAmount;
            inventory.DateUpdate = DateTime.UtcNow;
            await _inventoryRepository.UpdateAndSave(inventory);

            // 4) Notificar si queda < 20
            if (inventory.AvailableQuantity < LowStockThresholdExclusive)
            {
                productosFaltantesConteo.Add(new ProductoFaltanteConteoDto { CantidadDisponible = (int)inventory.AvailableQuantity, ProductName = productName });
            }
        }
    }
}
