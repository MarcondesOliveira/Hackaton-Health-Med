using Hackaton.Domain.Dto;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetConsultas
{
    public class GetConsultasQuery : IRequest<List<ConsultaDto>>
    {
    }
}
