
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


        public async Task<List<GetReservationsDto>> GetReservationsByDate(DateTime date)
        {
            var targetDate = date.Date;

            return await _dbContext.Reservations
                .Include(r => r.Table)
                .Include(r => r.Customer)
                .Where(r => r.DateReservation.Date == targetDate)
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
                    Created = (DateTime)r.Created,
                    StatusReservation = r.StatusReservation
                })
                .ToListAsync();
        }

        public async Task<List<GetReservationsDto>> GetReservationsByCustomerId(int customerId)
        {
            return await _dbContext.Reservations
                .Include(r => r.Table)
                .Include(r => r.Customer)
                .Where(r => r.IdCustomer == customerId)
                .OrderByDescending(r => r.Created)
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
                    Created = (DateTime)r.Created,
                    StatusReservation = r.StatusReservation
                })
                .ToListAsync();
        }



    }
}
