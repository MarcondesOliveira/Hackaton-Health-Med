using AutoMapper;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Commands.CreateConsulta
{
    public class CreateConsultaCommandHandler : IRequestHandler<CreateConsultaCommand, Guid>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public CreateConsultaCommandHandler(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = _mapper.Map<Consulta>(request);
            consulta.ConsultaId = Guid.NewGuid(); // Gerar o ID manualmente
            //consulta.Status = Status.Disponivel; // Definir status padrão como 'Disponivel'

            await _consultaRepository.AddAsync(consulta);
            return consulta.ConsultaId;
        }
    }

}
