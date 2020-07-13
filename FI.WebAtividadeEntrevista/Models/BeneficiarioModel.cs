using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.Models.CustomValidation;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "O CPF é obrigatório.")]
		[CustomValidationCPF(ErrorMessage = "Informe um CPF válido.")]
		[Display(Name = "CPF")]
		public string CPFBeneficiario { get; set; }

		[Required(ErrorMessage = "O nome do beneficiário é obrigatório.")]
		[Display(Name = "Nome")]
		public string NomeBeneficiario { get; set; }

		public long IdCliente { get; set; }
	}
}