using AutoMapper;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using MediatR;

namespace Hackaton.Application.Features.Commands.UpdateConsulta
{
    public class UpdateConsultaCommandHandler : IRequestHandler<UpdateConsultaCommand, bool>
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UpdateConsultaCommandHandler(IConsultaRepository consultaRepository, IMedicoRepository medicoRepository, IPacienteRepository pacienteRepository, IMapper mapper, IEmailService emailService)
        {
            _consultaRepository = consultaRepository;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> Handle(UpdateConsultaCommand request, CancellationToken cancellationToken)
        {
            var consulta = await _consultaRepository.GetByIdAsync(request.ConsultaId); // Obtem a consulta pelo ID
            if (consulta == null)
            {
                return false; 
            }

            consulta.PacienteId = request.PacienteId;
            consulta.Data = request.Data; 
            consulta.Status = request.Status;
            
            await _consultaRepository.UpdateAsync(consulta);

            var medico = await _medicoRepository.GetByIdAsync(request.MedicoId);
            if (medico == null)
            {
                return false; // Retorna falso se o médico não for encontrado
            }

            var paciente = await _pacienteRepository.GetByIdAsync(request.PacienteId);
            if (paciente == null)
            {
                return false; // Retorna falso se o paciente não for encontrado
            }

            // Formatar o email
            string tituloEmail = "Health&Med - Nova consulta agendada";
            string corpoEmail = $"Olá, Dr. {medico.Nome}!\n" + // Usar o nome do médico
                                $"Você tem uma nova consulta marcada! Paciente: {paciente.Nome}.\n" + // Nome do paciente
                                $"Data e horário: {request.Data.ToString("d")} às {request.Data.ToString("t")}.";

            // Enviar o email
            await _emailService.SendEmailAsync(medico.Email, tituloEmail, corpoEmail);

            return true; 
        }
    }

}
