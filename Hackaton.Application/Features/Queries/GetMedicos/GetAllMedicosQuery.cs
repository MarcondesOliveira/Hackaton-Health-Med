using Hackaton.Domain.Dto;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetMedicos
{
    public class GetAllMedicosQuery : IRequest<List<MedicoDto>>
    {
    }
}
