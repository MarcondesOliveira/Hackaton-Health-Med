using MediatR;

namespace Hackaton.Application.Features.Commands.CreatePaciente
{
    public class CreatePacienteCommand : IRequest<Guid>
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
