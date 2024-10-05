using MediatR;

namespace Hackaton.Application.Features.Commands.LoginMedico
{
    public class LoginMedicoCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

}
