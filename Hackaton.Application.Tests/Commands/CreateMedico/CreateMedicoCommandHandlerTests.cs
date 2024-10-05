using AutoMapper;
using Hackaton.Application.Features.Commands.CreateMedico;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Moq;

namespace Hackaton.Application.Tests.Commands.CreateMedico
{
    public class CreateMedicoCommandHandlerTests
    {
        private readonly Mock<IMedicoRepository> _mockMedicoRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateMedicoCommandHandler _handler;

        public CreateMedicoCommandHandlerTests()
        {
            _mockMedicoRepository = new Mock<IMedicoRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateMedicoCommandHandler(_mockMedicoRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNewGuid_WhenMedicoIsSuccessfullyCreated()
        {
            // Arrange
            var command = new CreateMedicoCommand
            {
                CRM = "123456",
                CPF = "12345678901",
                Email = "medico@teste.com",
                Nome = "Dr. Teste",
                Senha = "senhaSegura"
            };

            var medico = new Medico();
            _mockMapper.Setup(m => m.Map<Medico>(command)).Returns(medico);

            _mockMedicoRepository.Setup(r => r.AddAsync(It.IsAny<Medico>()))
                .ReturnsAsync(medico); // Simular o retorno da criação do médico

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // O GUID gerado não pode ser vazio
            _mockMedicoRepository.Verify(r => r.AddAsync(It.IsAny<Medico>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldHashPassword_WhenMedicoIsCreated()
        {
            // Arrange
            var command = new CreateMedicoCommand
            {
                CRM = "123456",
                CPF = "12345678901",
                Email = "medico@teste.com",
                Nome = "Dr. Teste",
                Senha = "senhaSegura"
            };

            var medico = new Medico();
            _mockMapper.Setup(m => m.Map<Medico>(command)).Returns(medico);

            _mockMedicoRepository.Setup(r => r.AddAsync(It.IsAny<Medico>()))
                .ReturnsAsync(medico);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(BCrypt.Net.BCrypt.Verify(command.Senha, medico.Senha)); // Verifica se a senha foi corretamente hasheada
        }

        [Fact]
        public async Task Handle_ShouldInitializeConsultasList()
        {
            // Arrange
            var command = new CreateMedicoCommand
            {
                CRM = "123456",
                CPF = "12345678901",
                Email = "medico@teste.com",
                Nome = "Dr. Teste",
                Senha = "senhaSegura"
            };

            var medico = new Medico();
            _mockMapper.Setup(m => m.Map<Medico>(command)).Returns(medico);

            _mockMedicoRepository.Setup(r => r.AddAsync(It.IsAny<Medico>()))
                .ReturnsAsync(medico);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(medico.Consultas); // A lista de consultas não deve ser nula
            Assert.Empty(medico.Consultas); // A lista de consultas deve estar vazia inicialmente
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var command = new CreateMedicoCommand
            {
                CRM = "123456",
                CPF = "12345678901",
                Email = "medico@teste.com",
                Nome = "Dr. Teste",
                Senha = "senhaSegura"
            };

            var medico = new Medico();
            _mockMapper.Setup(m => m.Map<Medico>(command)).Returns(medico);

            _mockMedicoRepository.Setup(r => r.AddAsync(It.IsAny<Medico>()))
                .ThrowsAsync(new Exception("Erro ao adicionar médico"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }

}
