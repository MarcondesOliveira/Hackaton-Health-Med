using AutoMapper;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetMedicoById
{
    public class GetMedicoByIdQueryHandler : IRequestHandler<GetMedicoByIdQuery, MedicoDto>
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMapper _mapper;

        public GetMedicoByIdQueryHandler(IMedicoRepository medicoRepository, IMapper mapper)
        {
            _medicoRepository = medicoRepository;
            _mapper = mapper;
        }

        public async Task<MedicoDto> Handle(GetMedicoByIdQuery request, CancellationToken cancellationToken)
        {
            var medico = await _medicoRepository.GetByIdAsync(request.Id);
            if (medico == null)
            {
                return null;
            }

            return _mapper.Map<MedicoDto>(medico);
        }
    }
}
