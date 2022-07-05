using System.ComponentModel.DataAnnotations;

namespace APITeste.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário deve ser preenchido")]
        [MaxLength(20, ErrorMessage = "O nome deve ter de 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "O nome deve ter de 3 a 20 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "A senha deve ter de 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "A senha deve ter de 3 a 20 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}