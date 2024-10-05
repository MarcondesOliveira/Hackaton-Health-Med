using MediatR;

namespace Hackaton.Application.Features.Commands.LoginPaciente
{
    public class LoginPacienteCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
