
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Domain.Enums;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<GetOrdersDto>> GetOrders()
        {
            var today = DateTime.Today; // fecha actual con hora 00:00:00

            var rawOrders = await _dbContext.Order
                .Where(x => x.DateOrder.Date == today) // 📅 Filtra solo las órdenes del día
                .OrderByDescending(x => x.DateOrder) // 🔁 Ordena por fecha descendente
                .Select(x => new {
                    x.Id,
                    CustomerName = x.CustomerName,
                    CustomerNameV2 = x.Customer != null
        ? ((x.Customer.Names ?? "") + " " + (x.Customer.LastNames ?? "")).Trim()
        : "",
                    IdCustomer = x.Customer != null ? x.Customer.Id : 0,
                    x.DateOrder,
                    EmployeName = x.User.Names + " " + x.User.LastNames,
                    x.OrderType,
                    x.StatusOrder,
                    x.IdPaymentMethod,
                    x.Total
                })
                .ToListAsync();

            var result = rawOrders.Select(x => new GetOrdersDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                CustomerNameV2 = x.CustomerNameV2,
                IdCustomer = x.IdCustomer,
                DateOrder = x.DateOrder,
                EmployeName = x.EmployeName,
                OrderType = x.OrderType,
                IdPaymentMethod = x.IdPaymentMethod,
                StatusOrder = (int)x.StatusOrder,
                Total = x.Total
            }).ToList();

            return result;
        }

        public async Task<List<GetOrdersDto>> GetAllOrders()
        {
            var rawOrders = await _dbContext.Order
                .OrderByDescending(x => x.DateOrder)
                .Select(x => new {
                    x.Id,
                    CustomerName = x.CustomerName ?? "",    // si es null, devuelve vacío
                    IdCustomer = x.Customer != null ? x.Customer.Id : (int?)null, // nullable
                    x.DateOrder,
                    EmployeName = x.User != null ? (x.User.Names + " " + x.User.LastNames) : "",
                    x.OrderType,
                    x.StatusOrder,
                    x.Total
                })
                .ToListAsync();

            var result = rawOrders.Select(x => new GetOrdersDto
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                IdCustomer = x.IdCustomer, // puede ser null, depende de tu DTO
                DateOrder = x.DateOrder,
                EmployeName = x.EmployeName,
                OrderType = x.OrderType,
                StatusOrder = (int)x.StatusOrder, // si es null lo mando como 0
                Total = x.Total
            }).ToList();

            return result;
        }

        public async Task<List<GetOrderWithDetailsDto>> GetAllOrdersWithDetails()
        {
            var orders = await _dbContext.Order
                .AsNoTracking()
                .OrderByDescending(o => o.DateOrder)
                .Select(o => new GetOrderWithDetailsDto
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName ?? "",
                    IdCustomer = o.Customer != null ? o.Customer.Id : (int?)null,
                    DateOrder = o.DateOrder,
                    EmployeName = o.User != null ? (o.User.Names + " " + o.User.LastNames) : "",
                    OrderType = o.OrderType,
                    StatusOrder = (int)o.StatusOrder,
                    Total = o.Total,
                    IdPaymentMethod = o.IdPaymentMethod ?? 0,

                    OrderDetails = o.OrderDetails.Select(od => new GetOrderDetailDto
                    {
                        Id = od.Id,
                        IdProduct = od.IdProduct,
                        ProductName = od.Product != null ? od.Product.Name : "",
                        Amount = od.Amount,
                        UnitPrice = od.UnitPrice,
                        SubTotal = od.SubTotal,
                        IdCategory = od.Product != null ? (int?)od.Product.Category.Id : null,
                        CategoryName = od.Product != null ? od.Product.Category.Name : ""
                    }).ToList()
                })
                .ToListAsync();

            return orders;
        }

        public async Task<GetOrderWithDetailsDto?> GetOrderWithDetailsById(int orderId)
        {
            return await _dbContext.Order
                .AsNoTracking()
                .Where(o => o.Id == orderId)
                .Select(o => new GetOrderWithDetailsDto
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName ?? "",
                    IdCustomer = o.Customer != null ? o.Customer.Id : (int?)null,
                    DateOrder = o.DateOrder,
                    EmployeName = o.User != null ? (o.User.Names + " " + o.User.LastNames) : "",
                    OrderType = o.OrderType,
                    StatusOrder = (int)o.StatusOrder,
                    Total = o.Total,
                    IdPaymentMethod = o.IdPaymentMethod ?? 0,
                    OrderDetails = o.OrderDetails.Select(od => new GetOrderDetailDto
                    {
                        Id = od.Id,
                        IdProduct = od.IdProduct,
                        ProductName = od.Product != null ? od.Product.Name : "",
                        Amount = od.Amount,
                        UnitPrice = od.UnitPrice,
                        SubTotal = od.SubTotal,
                        IdCategory = od.Product != null ? (int?)od.Product.Category.Id : null,
                        CategoryName = od.Product != null ? od.Product.Category.Name : ""
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }



        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var totalSales = await _dbContext.Order
                .Where(o => o.Created >= today && o.Created < tomorrow && o.StatusOrder == OrderStatus.PAGADO)
                .SumAsync(o => (decimal?)o.Total) ?? 0;
            return totalSales;
        }

        public async Task<ReporteToday> GetTodayPaymentsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            var paymentsToday = _dbContext.Payments
                .Where(p => p.Created >= today && p.Created < tomorrow);

            var totalPayments = await paymentsToday.SumAsync(p => (decimal?)p.AmountPaid) ?? 0;
            var paymentCount = await paymentsToday.CountAsync();

            return new ReporteToday
            {
                totalPayments = totalPayments,
                paymentCount = paymentCount
            };
        }

        public async Task<List<CategorySalesDto>> GetTodaySalesByCategoryAsync(DateTime? date = null)
        {
            var targetDate = date?.Date ?? DateTime.Today;
            var nextDate = targetDate.AddDays(1);

            return await _dbContext.OrderDetail
                .Where(od => od.Order.DateOrder >= targetDate && od.Order.DateOrder < nextDate)
                .GroupBy(od => new
                {
                    od.Product.Category.Id,
                    od.Product.Category.Name
                })
                .Select(g => new CategorySalesDto
                {
                    CategoryId = g.Key.Id,
                    CategoryName = g.Key.Name,
                    TotalSales = g.Sum(x => x.SubTotal),
                    TotalItemsSold = g.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.TotalSales)
                .ToListAsync();
        }

        public async Task<List<ProductSalesDto>> GetTodaySalesByProductAsync(DateTime? date = null)
        {
            var targetDate = date?.Date ?? DateTime.Today;
            var nextDate = targetDate.AddDays(1);

            return await _dbContext.OrderDetail
                .Where(od => od.Order.DateOrder >= targetDate && od.Order.DateOrder < nextDate)
                .GroupBy(od => od.Product.Name)
                .Select(g => new ProductSalesDto
                {
                    ProductName = g.Key,
                    CantidadVendidas = g.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.CantidadVendidas)
                .ToListAsync();
        }


        public async Task<List<GetOrdersDto>> GetOrdersByCustomerId(int customerId)
        {
            var orders = await _dbContext.Order
                .Where(x => x.IdCustomer == customerId)
                .OrderByDescending(x => x.DateOrder)
                .Select(x => new GetOrdersDto
                {
                    Id = x.Id,
                    CustomerName = x.Customer.Names,
                    IdCustomer = x.Customer.Id,
                    DateOrder = x.DateOrder,
                    EmployeName = x.User.Names + " " + x.User.LastNames,
                    OrderType = x.OrderType,
                    StatusOrder = (int)x.StatusOrder,
                    Total = x.Total
                })
                .ToListAsync();

            return orders;
        }

        public async Task<SalesByMonthResponseDto> GetSalesByMonthAsync(int year, int month, bool onlyPaid = true)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            var baseQuery = _dbContext.OrderDetail
                .AsNoTracking()
                .Where(od => od.Order.DateOrder >= start && od.Order.DateOrder < end);

            if (onlyPaid)
                baseQuery = baseQuery.Where(od => od.Order.StatusOrder == OrderStatus.PAGADO);

            var items = await baseQuery
                .GroupBy(od => new
                {
                    od.IdProduct,
                    ProductName = od.Product.Name,
                    CategoryId = od.Product.Category.Id,
                    CategoryName = od.Product.Category.Name
                })
                .Select(g => new SalesByMonthItemDto
                {
                    ProductId = g.Key.IdProduct,
                    ProductName = g.Key.ProductName,
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    VecesVendido = g.Count(),                 // o: g.Select(x => x.IdOrder).Distinct().Count()
                    CantidadVendida = g.Sum(x => x.Amount),
                    TotalProducto = g.Sum(x => x.SubTotal)
                })
                .OrderByDescending(x => x.TotalProducto)
                .ToListAsync();

            return new SalesByMonthResponseDto
            {
                Year = year,
                Month = month,
                Items = items,
                TotalDelMes = items.Sum(x => x.TotalProducto)
            };
        }


        public async Task<SalesByDateResponseDto> GetSalesByDateAsync(DateTime date, bool onlyPaid = true)
        {
            var targetDate = date.Date;
            var nextDate = targetDate.AddDays(1);

            // Base query: detalles dentro del día
            var baseQuery = _dbContext.OrderDetail
                .AsNoTracking()
                .Where(od => od.Order.DateOrder >= targetDate && od.Order.DateOrder < nextDate);

            if (onlyPaid)
            {
                baseQuery = baseQuery.Where(od => od.Order.StatusOrder == OrderStatus.PAGADO);
            }

            // Agrupamos por Producto + Categoría
            var items = await baseQuery
                .GroupBy(od => new
                {
                    od.IdProduct,
                    ProductName = od.Product.Name,
                    CategoryId = od.Product.Category.Id,
                    CategoryName = od.Product.Category.Name
                })
                .Select(g => new SalesByDateItemDto
                {
                    ProductId = g.Key.IdProduct,
                    ProductName = g.Key.ProductName,
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    VecesVendido = g.Count(),                    // número de líneas de detalle del producto
                    CantidadVendida = g.Sum(x => x.Amount),      // suma de unidades vendidas
                    TotalProducto = g.Sum(x => x.SubTotal)       // suma de subtotales
                })
                .OrderByDescending(x => x.TotalProducto)
                .ToListAsync();

            var totalDelDia = items.Sum(x => x.TotalProducto);

            return new SalesByDateResponseDto
            {
                Date = targetDate,
                Items = items,
                TotalDelDia = totalDelDia
            };
        }




    }
}
