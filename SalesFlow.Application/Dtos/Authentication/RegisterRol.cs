using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Dtos.Authentication
{
    public class AddOrUpdateRol
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
