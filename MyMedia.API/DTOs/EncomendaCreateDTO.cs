using System.ComponentModel.DataAnnotations;

namespace MyMedia.API.DTOs {
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