using AutoMapper;
using Hackaton.Application.Features.Queries.GetMedicos;
using Hackaton.Domain.Dto;
using Hackaton.Domain.Entities;
using Hackaton.Domain.Interfaces;
using Moq;

namespace Hackaton.Application.Tests.Queries.GetMedicos
{
    public class GetAllMedicosQueryHandlerTests
    {
        private readonly Mock<IMedicoRepository> _mockMedicoRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllMedicosQueryHandler _handler;

        public GetAllMedicosQueryHandlerTests()
        {
            _mockMedicoRepository = new Mock<IMedicoRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAllMedicosQueryHandler(_mockMedicoRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfMedicoDto_WhenMedicosExist()
        {
            // Arrange
            var medicos = new List<Medico>
        {
            new Medico { MedicoId = Guid.NewGuid(), Nome = "Dr. João", CPF = "12345678900", CRM = "CRM123", Email = "joao@exemplo.com" },
            new Medico { MedicoId = Guid.NewGuid(), Nome = "Dra. Maria", CPF = "09876543211", CRM = "CRM456", Email = "maria@exemplo.com" }
        };

            var medicoDtos = new List<MedicoDto>
        {
            new MedicoDto { MedicoId = medicos[0].MedicoId, Nome = medicos[0].Nome, CPF = medicos[0].CPF, CRM = medicos[0].CRM, Email = medicos[0].Email },
            new MedicoDto { MedicoId = medicos[1].MedicoId, Nome = medicos[1].Nome, CPF = medicos[1].CPF, CRM = medicos[1].CRM, Email = medicos[1].Email }
        };

            _mockMedicoRepository.Setup(r => r.ListAllWithConsultasAsync()).ReturnsAsync(medicos);
            _mockMapper.Setup(m => m.Map<List<MedicoDto>>(medicos)).Returns(medicoDtos);

            var query = new GetAllMedicosQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result); // Verifica que o resultado não é nulo
            Assert.Equal(2, result.Count); // Verifica que retornou 2 médicos
            Assert.Equal(medicoDtos, result); // Verifica que o resultado mapeado está correto
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoMedicosExist()
        {
            // Arrange
            var medicos = new List<Medico>(); // Lista vazia

            _mockMedicoRepository.Setup(r => r.ListAllWithConsultasAsync()).ReturnsAsync(medicos);
            _mockMapper.Setup(m => m.Map<List<MedicoDto>>(medicos)).Returns(new List<MedicoDto>()); // Mapeia para lista vazia

            var query = new GetAllMedicosQuery();

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
            var medicos = new List<Medico>(); // Lista vazia
            _mockMedicoRepository.Setup(r => r.ListAllWithConsultasAsync()).ReturnsAsync(medicos);

            var query = new GetAllMedicosQuery();

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert
            _mockMedicoRepository.Verify(r => r.ListAllWithConsultasAsync(), Times.Once); // Verifica que o método do repositório foi chamado uma vez
            _mockMapper.Verify(m => m.Map<List<MedicoDto>>(medicos), Times.Once); // Verifica que o método de mapeamento foi chamado uma vez
        }
    }
}
