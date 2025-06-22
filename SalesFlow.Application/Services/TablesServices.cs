using SalesFlow.Application.Dtos;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using System.Net;

namespace SalesFlow.Application.Services
{
    public class TablesServices : ITablesServices
    {

        private readonly ITableRepository tableRepository;

        public TablesServices(ITableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
        }

        public async Task<ApiResponse<List<GetTablesDto>>> GetTables()
        {
           var response = await tableRepository.GetTables();
           return new ApiResponse<List<GetTablesDto>>(response);
        }

        public async Task<ApiResponse<string>>Add(AddEditTablesDto dto)
        {
            var data = new Tables
            {
                Capacity = dto.Capacity,
                Name = dto.Name,    
                StatusTable = dto.StatusTable
            };

            await tableRepository.InsertAndSave(data);
            return new ApiResponse<string>("Registro guardado correctamente");
        }


        public async Task<ApiResponse<string>> Update(AddEditTablesDto dto)
        {

            var dataUpdate = await tableRepository.Get(c => c.Id == dto.Id);
            if (dataUpdate == null)
                throw new ApiException("Mesa not found", (int)HttpStatusCode.NotFound);

            dataUpdate.StatusTable = dto.StatusTable;
            dataUpdate.Name = dto.Name; 
            dataUpdate.Capacity = dto.Capacity;

            await tableRepository.UpdateAndSave(dataUpdate);
            return new ApiResponse<string>("Registro actualizado correctamente");
        }


    }
}
