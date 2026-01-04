using System.ComponentModel.DataAnnotations;

namespace RCLComum.Models {
    public class ProdutoCreateDTO {
        [Required]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        [Required]
        public decimal PrecoBase { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public int CategoriaId { get; set; }
    }
}