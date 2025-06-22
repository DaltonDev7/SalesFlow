using SalesFlow.Application.Dtos;
using SalesFlow.Application.Exception;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Application.Services
{
    public class ReservationsServices : IReservationsServices
    {
        private readonly IReservationRepository reservationRepository;


        public ReservationsServices(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public async Task<ApiResponse<List<GetReservationsDto>>> GetReservations()
        {
            var reservations = await reservationRepository.GetReservations();
            return new ApiResponse<List<GetReservationsDto>>(reservations);
        }

        public async Task<ApiResponse<string>> Add(AddEditReservations dto)
        {
            var reservation = new Reservations
            {
                IdCustomer = dto.IdCustomer,
                IdTable = dto.IdTable,
                DateReservation = dto.DateReservation.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                StatusReservation = dto.StatusReservation
            };

            await reservationRepository.InsertAndSave(reservation);
            return new ApiResponse<string>("Reserva creada correctamente");
        }

        public async Task<ApiResponse<string>> Update(AddEditReservations dto)
        {
            var existing = await reservationRepository.Get(r => r.Id == dto.Id);
            if (existing == null)
                throw new ApiException("Reserva no encontrada", (int)HttpStatusCode.NotFound);

            existing.IdCustomer = dto.IdCustomer;
            existing.IdTable = dto.IdTable;
            existing.DateReservation = dto.DateReservation.Date;
            existing.StartTime = dto.StartTime;
            existing.EndTime = dto.EndTime;
            existing.StatusReservation = dto.StatusReservation;

            await reservationRepository.UpdateAndSave(existing);
            return new ApiResponse<string>("Reserva actualizada correctamente");
        }
    }
}
