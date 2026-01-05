using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMedia.Data.Models {
    public enum EstadoProduto { Pendente, Ativo, Inativo }

    public class Produto {
        public int ProdutoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Stock { get; set; }
        public bool ParaVenda { get; set; }
        public EstadoProduto Estado { get; set; } = EstadoProduto.Pendente;

        public decimal PrecoBase { get; set; } 
        public decimal PrecoFinal { get; set; } 

        public string FornecedorId { get; set; } 
        public ApplicationUser Fornecedor { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int ModoDisponibilidadeId { get; set; }
        public virtual ModoDisponibilidade ModoDisponibilidade { get; set; }
    }
}
