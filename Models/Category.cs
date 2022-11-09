using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDataDriven.Models
{
    [Table("category")]
    public class Category
    {
        public Category(string name, string description, string code)
        {
            this.Name = name;
            this.Description = description;
            this.Code = code;
        }

        public Category() { }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter no máximo 60 caracteres.")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter no mínimo 3 caracteres.")]

        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "O código da categoria é obrigatório.")]
        public string Code { get; set; }
    }
}