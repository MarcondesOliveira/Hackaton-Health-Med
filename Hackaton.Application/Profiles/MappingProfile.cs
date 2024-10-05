﻿using AutoMapper;
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
            // Mapeamento entre Medico e MedicoDto
            CreateMap<Medico, MedicoDto>();

            // Mapeamento entre Consulta e ConsultaDto
            CreateMap<Consulta, ConsultaDto>();

            // Mapeamento de CreateMedicoCommand para Medico
            CreateMap<CreateMedicoCommand, Medico>()
                .ForMember(dest => dest.MedicoId, opt => opt.Ignore()) // MedicoId será gerado no handler
                .ForMember(dest => dest.Consultas, opt => opt.Ignore()); // Consultas será uma lista vazia

            // Mapeamento de CreateConsultaCommand para Consulta
            CreateMap<CreateConsultaCommand, Consulta>()
                .ForMember(dest => dest.ConsultaId, opt => opt.Ignore()) // ConsultaId será gerado no handler
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore()) // PacienteId será preenchido posteriormente
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Disponivel)); // Definir status padrão como 'Disponível'


            // Mapeamento entre Paciente e PacienteDto
            CreateMap<Paciente, PacienteDto>();

            // Mapeamento de CreatePacienteCommand para Paciente
            CreateMap<CreatePacienteCommand, Paciente>()
                .ForMember(dest => dest.PacienteId, opt => opt.Ignore()); // PacienteId será gerado no handler
        }
    }
}
