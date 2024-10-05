using Hackaton.Domain.Entities;
using MediatR;

namespace Hackaton.Application.Features.Commands.UpdateConsulta
{
    public class UpdateConsultaCommand : IRequest<bool> 
    {
        public Guid ConsultaId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid PacienteId { get; set; }
        public Status Status { get; set; }
        public DateTime Data { get; set; } 
    }
}
