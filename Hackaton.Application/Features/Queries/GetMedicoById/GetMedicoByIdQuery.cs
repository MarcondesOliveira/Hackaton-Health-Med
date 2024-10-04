using Hackaton.Domain.Dto;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetMedicoById
{
    public class GetMedicoByIdQuery : IRequest<MedicoDto>
    {
        public Guid Id { get; set; }
    }
}
