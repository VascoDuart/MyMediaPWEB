using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMedia.Data.Models {
    public class Encomenda {
        public int EncomendaId { get; set; }
        public DateTime DataEncomenda { get; set; }
        public decimal ValorTotal { get; set; }

        public string EstadoEncomenda { get; set; }

        public string ClienteId { get; set; }
        public ApplicationUser Cliente { get; set; }

        public ICollection<ItemEncomenda> Itens { get; set; }
    }
}
