namespace Hackaton.Domain.Entities
{
    public class Consulta
    {
        public Guid ConsultaId { get; set; }
        public Guid MedicoId { get; set; }
        public Guid PacienteId { get; set; }
        public DateTime Data { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Disponivel,
        Agendada,
        Atendida
    }
}