using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.Validators;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public string ModalNome { get; internal set; }
        //[CpfCustomValidator(ErrorMessage = "Digite um cpf válido!")]
        public string ModalCPF { get; internal set; }
        public long ModalId { get; internal set; }
    }
}