﻿using AutoMapper;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Queries.GetConsultas
{
    public class GetConsultasQueryHandler : IRequestHandler<GetConsultasQuery, List<ConsultaDto>>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMapper _mapper;

        public GetConsultasQueryHandler(IConsultaRepository consultaRepository, IMapper mapper)
        {
            _consultaRepository = consultaRepository;
            _mapper = mapper;
        }

        public async Task<List<ConsultaDto>> Handle(GetConsultasQuery request, CancellationToken cancellationToken)
        {
            var consultas = await _consultaRepository.ListAllAsync();

            return _mapper.Map<List<ConsultaDto>>(consultas);
        }
    }
}