using System.ComponentModel.DataAnnotations;

namespace APITeste.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve ter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve ter de 3 a 60 caracteres")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Esse campo só suporta 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço não pode ser zero")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}