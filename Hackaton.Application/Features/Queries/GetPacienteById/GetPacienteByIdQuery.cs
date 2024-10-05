using Hackaton.Domain.Dto;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetPacienteById
{
    public class GetPacienteByIdQuery : IRequest<PacienteDto>
    {
        public Guid Id { get; set; }
    }
}
