using AutoMapper;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Commands.CreatePaciente
{
    public class CreatePacienteCommandHandler : IRequestHandler<CreatePacienteCommand, Guid>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;

        public CreatePacienteCommandHandler(IPacienteRepository pacienteRepository, IMapper mapper)
        {
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePacienteCommand request, CancellationToken cancellationToken)
        {
            var paciente = _mapper.Map<Paciente>(request);
            paciente.PacienteId = Guid.NewGuid();
            paciente.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            //paciente.Consultas = new List<Consulta>();

            await _pacienteRepository.AddAsync(paciente);

            return paciente.PacienteId;
        }
    }
}
