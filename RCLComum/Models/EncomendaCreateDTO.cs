using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLComum.Models {
    public class EncomendaCreateDTO {
        [Required]
        public List<ItemCarrinhoDTO> Itens { get; set; }
    }

    public class ItemCarrinhoDTO {
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
