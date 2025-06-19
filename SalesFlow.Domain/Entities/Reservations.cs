

using SalesFlow.Domain.Common;

namespace SalesFlow.Domain.Entities
{
    public class Reservations : BaseEntity
    {
        public int IdCustomer {  get; set; }
        public int IdTable { get; set;}

        public DateTime DateReservation { get; set; } // Solo la fecha
        public TimeOnly StartTime { get; set; }        // Hora de inicio
        public TimeOnly EndTime { get; set; }          // Hora de fin

        public string StatusReservation { get; set;}
    }
}
