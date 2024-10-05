using AutoMapper;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetPacienteById
{
    public class GetPacienteByIdQueryHandler : IRequestHandler<GetPacienteByIdQuery, PacienteDto>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public GetPacienteByIdQueryHandler(IPacienteRepository pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        public async Task<PacienteDto> Handle(GetPacienteByIdQuery request, CancellationToken cancellationToken)
        {
            var paciente = await _pacienteRepository.GetByIdAsync(request.Id);
            if (paciente == null)
            {
                return null;
            }

            return _mapper.Map<PacienteDto>(paciente);
        }
    }
}
