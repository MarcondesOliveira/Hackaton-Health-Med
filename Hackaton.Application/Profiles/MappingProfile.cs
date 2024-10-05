using AutoMapper;
using Hackaton.Application.Features.Commands.CreateConsulta;
using Hackaton.Application.Features.Commands.CreateMedico;
using Hackaton.Application.Features.Commands.CreatePaciente;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;

namespace Hackaton.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Medico, MedicoDto>()
                .ForMember(dest => dest.Consultas, opt => opt.MapFrom(src => src.Consultas));

            CreateMap<Consulta, ConsultaDto>();
                        
            CreateMap<CreateMedicoCommand, Medico>()
                .ForMember(dest => dest.MedicoId, opt => opt.Ignore()) // MedicoId será gerado no handler
                .ForMember(dest => dest.Consultas, opt => opt.Ignore()); // Consultas será uma lista vazia

            CreateMap<CreateConsultaCommand, Consulta>()
                .ForMember(dest => dest.ConsultaId, opt => opt.Ignore()) // ConsultaId será gerado no handler
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore()) // PacienteId será preenchido posteriormente
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Disponivel)); // Definir status padrão como 'Disponível'

            CreateMap<Paciente, PacienteDto>();

            CreateMap<CreatePacienteCommand, Paciente>()
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore());
        }
    }
}
