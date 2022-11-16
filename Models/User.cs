using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ApiDataDriven.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        public string Name { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

        [NotMapped]
        [DataMember]
        public string Token { get; set; }
    }

}