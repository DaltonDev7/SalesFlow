namespace SalesFlow.Application.Dtos
{
    public class GetReservationsDto
    {
        public int? Id { get; set; }
        public int IdCustomer { get; set; }
        public string CustomerName { get; set; }
        public string StatusReservation { get; set; }
        public int IdTable { get; set; }

        public string TableName { get; set; }
        public DateTime DateReservation { get; set; } // Solo la fecha
        public DateTime Created { get; set; } // Solo la fecha
        public TimeOnly StartTime { get; set; }        // Hora de inicio
        public TimeOnly EndTime { get; set; }          // Hora de fin
    }
}
