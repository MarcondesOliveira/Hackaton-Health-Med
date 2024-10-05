using AutoMapper;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Commands.UpdateConsulta
{
    public class UpdateConsultaCommandHandler : IRequestHandler<UpdateConsultaCommand, bool>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public UpdateConsultaCommandHandler(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = await _consultaRepository.GetByIdAsync(request.ConsultaId); // Obtem a consulta pelo ID
            if (consulta == null)
            {
                return false; 
            }

            consulta.Data = request.Data; 

            await _consultaRepository.UpdateAsync(consulta); 

            return true; 
        }
    }

}
