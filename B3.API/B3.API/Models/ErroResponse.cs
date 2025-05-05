namespace B3.API.Models
{
    public class ErroResponse
    {
        public int Status { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string? Detalhe { get; set; }
    }
}
