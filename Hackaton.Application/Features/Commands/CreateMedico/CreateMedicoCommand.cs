using MediatR;

namespace Hackaton.Application.Features.Commands.CreateMedico
{
    public class CreateMedicoCommand : IRequest<Guid>
    {
        public string CRM { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
