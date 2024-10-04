using Hackaton.Domain.Entities;

namespace Hackaton.Domain.Dto
{
    public class ConsultaDto
    {
        public Guid ConsultaId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime Data { get; set; }
        public Status Status { get; set; }
    }
}
