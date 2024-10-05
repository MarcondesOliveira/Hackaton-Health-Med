using AutoMapper;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetConsultaById
{
    public class GetConsultaByIdQueryHandler : IRequestHandler<GetConsultaByIdQuery, ConsultaDto>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public GetConsultaByIdQueryHandler(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<ConsultaDto> Handle(GetConsultaByIdQuery request, CancellationToken cancellationToken)
        {
            var consulta = await _consultaRepository.GetByIdAsync(request.ConsultaId); // Método do repositório

            return _mapper.Map<ConsultaDto>(consulta); // Mapeia a entidade para o DTO
        }
    }
}
