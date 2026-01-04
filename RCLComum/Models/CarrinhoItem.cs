using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Models {
    public class CarrinhoItem {
        public int ProdutoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public decimal Total => Preco * Quantidade;
    }
}
