using Hackaton.Domain.Dto;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetConsultaById
{
    public class GetConsultaByIdQuery : IRequest<ConsultaDto>
    {
        public Guid ConsultaId { get; set; }

        public GetConsultaByIdQuery(Guid consultaId)
        {
            ConsultaId = consultaId;
        }
    }
}
