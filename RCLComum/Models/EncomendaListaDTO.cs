using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Models {
    public class EncomendaListaDTO {
        public int EncomendaId { get; set; }
        public DateTime DataEncomenda { get; set; }
        public decimal ValorTotal { get; set; }
        public string EstadoEncomenda { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty; 
        public int QtdItens { get; set; }
    }
}
