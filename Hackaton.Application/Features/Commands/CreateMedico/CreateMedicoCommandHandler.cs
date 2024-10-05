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
            // Usando AutoMapper para mapear o comando para a entidade Medico
            var medico = _mapper.Map<Medico>(request);
            medico.MedicoId = Guid.NewGuid(); // Gerar o ID manualmente
            medico.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            medico.Consultas = new List<Consulta>(); // Inicializar a lista de Consultas

            await _medicoRepository.AddAsync(medico);

            return medico.MedicoId;
        }
    }
}

//public async Task<Guid> Handle(CreateMedicoCommand request, CancellationToken cancellationToken)
//{
//    var medico = _mapper.Map<Medico>(request); // Faz o mapeamento do objeto CreateMedicoCommand para Medico

//    medico.MedicoId = Guid.NewGuid();
//    medico.Consultas = new List<Consulta>();

//    await _medicoRepository.AddAsync(medico);

//    return medico.MedicoId;
//}

//public async Task<Guid> Handle(CreateMedicoCommand request, CancellationToken cancellationToken)
//{
//    var medico = new Medico
//    {
//        MedicoId = Guid.NewGuid(),
//        CRM = request.CRM,
//        CPF = request.CPF,
//        Email = request.Email,
//        Nome = request.Nome,
//        Senha = request.Senha,
//        Consultas = new List<Consulta>()
//    };

//    await _medicoRepository.AddAsync(medico);

//    return medico.MedicoId;
//}