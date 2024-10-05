using AutoMapper;
using Hackaton.Application.Features.Commands.CreateConsulta;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Moq;

namespace Hackaton.Application.Tests.Commands.CreateConsulta
{
    public class CreateConsultaCommandHandlerTests
    {
        private readonly Mock<IConsultaRepository> _mockConsultaRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateConsultaCommandHandler _handler;

        public CreateConsultaCommandHandlerTests()
        {
            _mockConsultaRepository = new Mock<IConsultaRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateConsultaCommandHandler(_mockConsultaRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnNewGuid_WhenConsultaIsSuccessfullyCreated()
        {
            // Arrange
            var command = new CreateConsultaCommand
            {
                MedicoId = Guid.NewGuid(), // Definir um médico fictício
                Data = DateTime.Now.AddDays(1), // Data no futuro
                Status = Status.Disponivel // Definir status como 'Disponível'
            };

            var consulta = new Consulta();
            _mockMapper.Setup(m => m.Map<Consulta>(command)).Returns(consulta);

            _mockConsultaRepository.Setup(r => r.AddAsync(It.IsAny<Consulta>()))
                .ReturnsAsync(consulta); // Retorna a consulta criada

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // O ID deve ser gerado e não pode ser vazio
            _mockConsultaRepository.Verify(r => r.AddAsync(It.IsAny<Consulta>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldMapConsultaFromCommand()
        {
            // Arrange
            var command = new CreateConsultaCommand
            {
                MedicoId = Guid.NewGuid(),
                Data = DateTime.Now.AddDays(1),
                Status = Status.Disponivel
            };

            var consulta = new Consulta();
            _mockMapper.Setup(m => m.Map<Consulta>(command)).Returns(consulta);

            _mockConsultaRepository.Setup(r => r.AddAsync(It.IsAny<Consulta>()))
                .ReturnsAsync(consulta); // Retorna a consulta criada

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockMapper.Verify(m => m.Map<Consulta>(command), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryFails()
        {
            // Arrange
            var command = new CreateConsultaCommand
            {
                MedicoId = Guid.NewGuid(),
                Data = DateTime.Now.AddDays(1),
                Status = Status.Disponivel
            };

            var consulta = new Consulta();
            _mockMapper.Setup(m => m.Map<Consulta>(command)).Returns(consulta);

            _mockConsultaRepository.Setup(r => r.AddAsync(It.IsAny<Consulta>()))
                .ThrowsAsync(new Exception("Erro ao adicionar consulta"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}


