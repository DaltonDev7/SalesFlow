using SalesFlow.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Interfaces.Services
{
    public interface IProductServices
    {
        Task<List<GetProductDto>> GetProductsByCategoryAsync(int categoryId);
    }
}
