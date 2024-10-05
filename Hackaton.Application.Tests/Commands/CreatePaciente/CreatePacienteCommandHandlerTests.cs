using AutoMapper;
using Hackaton.Application.Features.Commands.CreatePaciente;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Moq;

namespace Hackaton.Application.Tests.Commands.CreatePaciente
{
    public class CreatePacienteCommandHandlerTests
    {
        private readonly Mock<IPacienteRepository> _mockPacienteRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreatePacienteCommandHandler _handler;

        public CreatePacienteCommandHandlerTests()
        {
            _mockPacienteRepository = new Mock<IPacienteRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreatePacienteCommandHandler(_mockPacienteRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNewGuid_WhenPacienteIsSuccessfullyCreated()
        {
            // Arrange
            var command = new CreatePacienteCommand
            {
                Nome = "Paciente Teste",
                CPF = "12345678901",
                Email = "paciente@teste.com",
                Senha = "senhaSegura"
            };

            var paciente = new Paciente();
            _mockMapper.Setup(m => m.Map<Paciente>(command)).Returns(paciente);

            _mockPacienteRepository.Setup(r => r.AddAsync(It.IsAny<Paciente>()))
                .ReturnsAsync(paciente);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // O GUID gerado não pode ser vazio
            _mockPacienteRepository.Verify(r => r.AddAsync(It.IsAny<Paciente>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldHashPassword_WhenPacienteIsCreated()
        {
            // Arrange
            var command = new CreatePacienteCommand
            {
                Nome = "Paciente Teste",
                CPF = "12345678901",
                Email = "paciente@teste.com",
                Senha = "senhaSegura"
            };

            var paciente = new Paciente();
            _mockMapper.Setup(m => m.Map<Paciente>(command)).Returns(paciente);

            _mockPacienteRepository.Setup(r => r.AddAsync(It.IsAny<Paciente>()))
                .ReturnsAsync(paciente);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(BCrypt.Net.BCrypt.Verify(command.Senha, paciente.Senha)); // Verifica se a senha foi hasheada corretamente
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var command = new CreatePacienteCommand
            {
                Nome = "Paciente Teste",
                CPF = "12345678901",
                Email = "paciente@teste.com",
                Senha = "senhaSegura"
            };

            var paciente = new Paciente();
            _mockMapper.Setup(m => m.Map<Paciente>(command)).Returns(paciente);

            _mockPacienteRepository.Setup(r => r.AddAsync(It.IsAny<Paciente>()))
                .ThrowsAsync(new Exception("Erro ao adicionar paciente"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
