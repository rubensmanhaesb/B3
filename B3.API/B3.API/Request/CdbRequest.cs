using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace B3.API.Request
{
    public class CdbRequest
    {
        [Required(ErrorMessage = "Valor inicial é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Valor inicial deve ser maior que zero.")]
        [DefaultValue(0.01)]
        public decimal ValorInicial { get; set; } 

        [Required(ErrorMessage = "Prazo é obrigatório.")]
        [Range(2, int.MaxValue, ErrorMessage = "Prazo deve ser maior que 1 mês.")]
        [DefaultValue(6)]
        public int PrazoEmMeses { get; set; } 
    }
}
