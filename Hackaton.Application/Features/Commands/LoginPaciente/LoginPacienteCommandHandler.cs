using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Commands.LoginPaciente
{
    public class LoginPacienteCommandHandler : IRequestHandler<LoginPacienteCommand, string>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IJwtService _jwtService;

        public LoginPacienteCommandHandler(IPacienteRepository pacienteRepository, IJwtService jwtService)
        {
            _pacienteRepository = pacienteRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginPacienteCommand request, CancellationToken cancellationToken)
        {
            var paciente = await _pacienteRepository.GetByEmailAndPasswordAsync(request.Email, request.Senha);

            if (paciente == null)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            // Gera um token JWT para o médico
            return _jwtService.GenerateToken(paciente.PacienteId, paciente.Email, "Paciente");
        }
    }
}
