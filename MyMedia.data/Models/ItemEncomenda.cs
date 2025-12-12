using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMedia.Data.Models {
    public class ItemEncomenda {
        public int ItemEncomendaId { get; set; }
        public int Quantidade { get; set; }

        public decimal PrecoVendaUnitario { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int EncomendaId { get; set; }
        public Encomenda Encomenda { get; set; }
    }
}