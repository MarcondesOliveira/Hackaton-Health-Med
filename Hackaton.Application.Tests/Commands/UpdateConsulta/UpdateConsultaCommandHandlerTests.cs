using AutoMapper;
using Hackaton.Application.Features.Commands.UpdateConsulta;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Application.Tests.Commands.UpdateConsulta
{
    public class UpdateConsultaCommandHandlerTests
    {
        private readonly Mock<IConsultaRepository> _mockConsultaRepository;
        private readonly Mock<IMedicoRepository> _mockMedicoRepository;
        private readonly Mock<IPacienteRepository> _mockPacienteRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateConsultaCommandHandler _handler;

        public UpdateConsultaCommandHandlerTests()
        {
            _mockConsultaRepository = new Mock<IConsultaRepository>();
            _mockMedicoRepository = new Mock<IMedicoRepository>();
            _mockPacienteRepository = new Mock<IPacienteRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockMapper = new Mock<IMapper>();
            _handler = new UpdateConsultaCommandHandler(
                _mockConsultaRepository.Object,
                _mockMedicoRepository.Object,
                _mockPacienteRepository.Object,
                _mockMapper.Object,
                _mockEmailService.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenConsultaIsSuccessfullyUpdated()
        {
            // Arrange
            var command = new UpdateConsultaCommand
            {
                ConsultaId = Guid.NewGuid(),
                MedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
                Data = DateTime.Now,
                Status = Status.Agendada
            };

            var consulta = new Consulta();
            var medico = new Medico { Nome = "Dr. João", Email = "joao@exemplo.com" };
            var paciente = new Paciente { Nome = "Paciente Teste" };

            _mockConsultaRepository.Setup(r => r.GetByIdAsync(command.ConsultaId))
                .ReturnsAsync(consulta);
            _mockMedicoRepository.Setup(r => r.GetByIdAsync(command.MedicoId))
                .ReturnsAsync(medico);
            _mockPacienteRepository.Setup(r => r.GetByIdAsync(command.PacienteId))
                .ReturnsAsync(paciente);

            _mockEmailService.Setup(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result); // Verifica que o resultado é true
            _mockConsultaRepository.Verify(r => r.UpdateAsync(It.IsAny<Consulta>()), Times.Once);
            _mockEmailService.Verify(e => e.SendEmailAsync(medico.Email, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenConsultaNotFound()
        {
            // Arrange
            var command = new UpdateConsultaCommand
            {
                ConsultaId = Guid.NewGuid(),
                MedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
                Data = DateTime.Now,
                Status = Status.Agendada
            };

            _mockConsultaRepository.Setup(r => r.GetByIdAsync(command.ConsultaId))
                .ReturnsAsync((Consulta)null); // Consulta não encontrada

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result); // Verifica que o resultado é false
            _mockConsultaRepository.Verify(r => r.UpdateAsync(It.IsAny<Consulta>()), Times.Never);
            _mockEmailService.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldSendEmailToMedico_WhenConsultaIsUpdated()
        {
            // Arrange
            var command = new UpdateConsultaCommand
            {
                ConsultaId = Guid.NewGuid(),
                MedicoId = Guid.NewGuid(),
                PacienteId = Guid.NewGuid(),
                Data = DateTime.Now,
                Status = Status.Agendada
            };

            var consulta = new Consulta();
            var medico = new Medico { Nome = "Dr. João", Email = "joao@exemplo.com" };
            var paciente = new Paciente { Nome = "Paciente Teste" };

            _mockConsultaRepository.Setup(r => r.GetByIdAsync(command.ConsultaId))
                .ReturnsAsync(consulta);
            _mockMedicoRepository.Setup(r => r.GetByIdAsync(command.MedicoId))
                .ReturnsAsync(medico);
            _mockPacienteRepository.Setup(r => r.GetByIdAsync(command.PacienteId))
                .ReturnsAsync(paciente);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockEmailService.Verify(e => e.SendEmailAsync(
                medico.Email,
                "Health&Med - Nova consulta agendada",
                It.Is<string>(body => body.Contains(medico.Nome) && body.Contains(paciente.Nome))
            ), Times.Once); // Verifica que o e-mail foi enviado com os dados corretos
        }
    }
}
