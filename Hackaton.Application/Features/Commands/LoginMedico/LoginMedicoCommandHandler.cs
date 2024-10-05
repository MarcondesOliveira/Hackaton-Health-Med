using Hackaton.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Application.Features.Commands.LoginMedico
{
    public class LoginMedicoCommandHandler : IRequestHandler<LoginMedicoCommand, string>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IJwtService _jwtService;

        public LoginMedicoCommandHandler(IMedicoRepository medicoRepository, IJwtService jwtService)
        {
            _medicoRepository = medicoRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(LoginMedicoCommand request, CancellationToken cancellationToken)
        {
            var medico = await _medicoRepository.GetByEmailAndPasswordAsync(request.Email, request.Senha);

            if (medico == null)
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            // Gera um token JWT para o médico
            return _jwtService.GenerateToken(medico.MedicoId, medico.Email);
        }
    }

}
