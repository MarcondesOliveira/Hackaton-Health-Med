using Hackaton.Domain.Entities;

namespace Hackaton.Domain.Dto
{
    public class MedicoDto
    {
        public Guid MedicoId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public List<ConsultaDto> Consultas { get; set; }
    }
}