using AutoMapper;
using Hackaton.Application.Features.Queries.GetConsultas;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Moq;

namespace Hackaton.Application.Tests.Queries.GetConsultas
{
    public class GetConsultasQueryHandlerTests
    {
        private readonly Mock<IConsultaRepository> _mockConsultaRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetConsultasQueryHandler _handler;

        public GetConsultasQueryHandlerTests()
        {
            _mockConsultaRepository = new Mock<IConsultaRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetConsultasQueryHandler(_mockConsultaRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfConsultaDto_WhenConsultasExist()
        {
            // Arrange
            var consultas = new List<Consulta>
        {
            new Consulta { ConsultaId = Guid.NewGuid(), MedicoId = Guid.NewGuid(), PacienteId = Guid.NewGuid(), Data = DateTime.Now, Status = Status.Disponivel },
            new Consulta { ConsultaId = Guid.NewGuid(), MedicoId = Guid.NewGuid(), PacienteId = Guid.NewGuid(), Data = DateTime.Now.AddDays(1), Status = Status.Agendada }
        };

            var consultaDtos = new List<ConsultaDto>
        {
            new ConsultaDto { ConsultaId = consultas[0].ConsultaId, MedicoId = consultas[0].MedicoId, PacienteId = consultas[0].PacienteId, Data = consultas[0].Data, Status = consultas[0].Status },
            new ConsultaDto { ConsultaId = consultas[1].ConsultaId, MedicoId = consultas[1].MedicoId, PacienteId = consultas[1].PacienteId, Data = consultas[1].Data, Status = consultas[1].Status }
        };

            _mockConsultaRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(consultas);
            _mockMapper.Setup(m => m.Map<List<ConsultaDto>>(consultas)).Returns(consultaDtos);

            var query = new GetConsultasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result); // Verifica que o resultado não é nulo
            Assert.Equal(2, result.Count); // Verifica que retornou 2 consultas
            Assert.Equal(consultaDtos, result); // Verifica que o resultado mapeado está correto
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoConsultasExist()
        {
            // Arrange
            var consultas = new List<Consulta>(); // Lista vazia

            _mockConsultaRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(consultas);
            _mockMapper.Setup(m => m.Map<List<ConsultaDto>>(consultas)).Returns(new List<ConsultaDto>()); // Mapeia para lista vazia

            var query = new GetConsultasQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result); // Verifica que o resultado não é nulo
            Assert.Empty(result); // Verifica que a lista retornada está vazia
        }

        [Fact]
        public async Task Handle_ShouldCallRepositoryAndMapperOnce()
        {
            // Arrange
            var consultas = new List<Consulta>(); // Lista vazia
            _mockConsultaRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(consultas);

            var query = new GetConsultasQuery();

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            _mockConsultaRepository.Verify(r => r.ListAllAsync(), Times.Once); // Verifica que o método do repositório foi chamado uma vez
            _mockMapper.Verify(m => m.Map<List<ConsultaDto>>(consultas), Times.Once); // Verifica que o método de mapeamento foi chamado uma vez
        }
    }
}
