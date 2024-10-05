using Hackaton.Domain.Entities;
using MediatR;

namespace Hackaton.Application.Features.Commands.CreateConsulta
{
    public class CreateConsultaCommand : IRequest<Guid>
    {
        public Guid MedicoId { get; set; }
        public DateTime Data { get; set; }
        public Status Status { get; set; }
    }

}
