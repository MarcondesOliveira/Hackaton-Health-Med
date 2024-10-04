namespace Hackaton.Domain.Entities
{
    public class Medico : Entity
    {
        public Guid MedicoId { get; set; }
        public string CRM { get; set; }

        public List<Consulta> Consultas { get; set; }
    }
}