using System.ComponentModel.DataAnnotations;

namespace APIshop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve ter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve ter de 3 a 60 caracteres")]
        public string Title { get; set; }
    }
}