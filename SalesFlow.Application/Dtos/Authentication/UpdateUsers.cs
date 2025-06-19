using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Dtos.Authentication
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
        public int IdRol { get; set; }
    }


}
