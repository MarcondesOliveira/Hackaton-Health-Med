using AutoMapper;
using BCrypt.Net;
using Hackaton.Application.Features.Commands.CreateMedico;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Commands.CreateMedico
{
    public class CreateMedicoCommandHandler : IRequestHandler<CreateMedicoCommand, Guid>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public CreateMedicoCommandHandler(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateMedicoCommand request, CancellationToken cancellationToken)
        {
            var medico = _mapper.Map<Medico>(request);
            medico.MedicoId = Guid.NewGuid(); 
            medico.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            medico.Consultas = new List<Consulta>();

            await _medicoRepository.AddAsync(medico);

            return medico.MedicoId;
        }
    }
}