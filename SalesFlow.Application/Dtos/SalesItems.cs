using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Dtos
{
    public class SalesByDateItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";

        // "Cuántas veces se vendió" -> cuántas líneas de OrderDetail tuvo ese producto
        public int VecesVendido { get; set; }

        // Cantidad total vendida (suma de Amount)
        public int CantidadVendida { get; set; }

        // Total por producto (suma de SubTotal)
        public decimal TotalProducto { get; set; }
    }

    public class SalesByDateResponseDto
    {
        public DateTime Date { get; set; }
        public List<SalesByDateItemDto> Items { get; set; } = new();
        public decimal TotalDelDia { get; set; }
    }

    public class SalesByMonthItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public int VecesVendido { get; set; }        // cuántas líneas de detalle del producto en el mes
        public int CantidadVendida { get; set; }     // suma de Amount
        public decimal TotalProducto { get; set; }   // suma de SubTotal
    }

    public class SalesByMonthResponseDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<SalesByMonthItemDto> Items { get; set; } = new();
        public decimal TotalDelMes { get; set; }
    }


}
