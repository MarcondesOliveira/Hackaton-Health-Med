using AutoMapper;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetMedicos
{
    public class GetAllMedicosQueryHandler : IRequestHandler<GetAllMedicosQuery, List<MedicoDto>>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public GetAllMedicosQueryHandler(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task<List<MedicoDto>> Handle(GetAllMedicosQuery request, CancellationToken cancellationToken)
        {
            var medicos = await _medicoRepository.ListAllAsync();
            return _mapper.Map<List<MedicoDto>>(medicos);
        }
    }
}
