using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDataDriven.Models
{
    class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(85, ErrorMessage = "Este campo deve conter no máximo 85 caracteres")]
        public string Name { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }
    }

}