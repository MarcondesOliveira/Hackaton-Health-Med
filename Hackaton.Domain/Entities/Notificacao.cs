using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Domain.Entities
{
    public class Notificacao
    {
        public string Mensagem { get; set; }
        //public Tipo Tipo { get; set; }
        public string Destinatario { get; set; }
    }
}
