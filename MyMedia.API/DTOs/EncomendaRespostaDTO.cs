namespace MyMedia.API.DTOs {
    public class EncomendaRespostaDTO {
        public int EncomendaId { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }
        public string Estado { get; set; }
        public string ClienteNome { get; set; }
        public int TotalItens { get; set; }
    }
}