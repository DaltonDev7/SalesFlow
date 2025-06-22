

namespace SalesFlow.Application.Dtos
{
    public class AddEditTablesDto
    {
        public int? Id { get; set; } 
        public string Name { get; set; }

        public int Capacity { get; set; }

        public string StatusTable { get; set; }
    }
}
