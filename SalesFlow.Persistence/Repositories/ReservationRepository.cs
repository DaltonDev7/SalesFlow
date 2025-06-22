
using Microsoft.EntityFrameworkCore;
using SalesFlow.Application.Dtos;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence.Repositories
{
    public class ReservationRepository : BaseRepository<Reservations>, IReservationRepository
    {
        public ReservationRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }


        public async Task<List<GetReservationsDto>> GetReservations()
        {
            return await _dbContext.Reservations
                .Include(r => r.Table)
                .Include(r => r.Customer) // Suponiendo que tienes una propiedad de navegación a ApplicationUser
                .Select(r => new GetReservationsDto
                {
                    Id = r.Id,
                    IdCustomer = r.IdCustomer,
                    CustomerName = r.Customer.Names + " " + r.Customer.LastNames,
                    IdTable = r.IdTable,
                    TableName = r.Table.Name,
                    DateReservation = r.DateReservation,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    StatusReservation = r.StatusReservation
                })
                .ToListAsync();
        }
    }
}
