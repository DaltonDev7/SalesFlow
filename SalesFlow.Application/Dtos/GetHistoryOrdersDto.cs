using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Dtos
{
    public class GetHistoryOrdersDto
    {
        public DateTime Fecha { get; set; }
        public int IdOrder { get; set; }

        public string NameCustomer { get; set; }

        public int Total { get; set; }
        public string MethodPayment { get; set; }

        public string OrderType { get; set; }
    }

    public class CreateHistoryOrdersDto
    {
        public DateTime Fecha { get; set; }
        public int IdOrder { get; set; }

        public string NameCustomer { get; set; }

        public int Total { get; set; }
        public string MethodPayment { get; set; }

        public string OrderType { get; set; }
    }

}
