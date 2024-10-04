using AutoMapper;
using Hackaton.Application.Features.Commands.CreateMedico;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;

namespace Hackaton.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento entre Medico e MedicoDto
            CreateMap<Medico, MedicoDto>();

            // Mapeamento entre Consulta e ConsultaDto
            CreateMap<Consulta, ConsultaDto>();

            // Mapeamento de CreateMedicoCommand para Medico
            CreateMap<CreateMedicoCommand, Medico>()
                .ForMember(dest => dest.MedicoId, opt => opt.Ignore()) // MedicoId será gerado no handler
                .ForMember(dest => dest.Consultas, opt => opt.Ignore()); // Consultas será uma lista vazia
        }
    }
}
