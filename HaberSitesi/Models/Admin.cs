using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required(ErrorMessage = "Kullanıcı Adı boş bırakılamaz!")]
        [DisplayName("Kullanıcı Adı")]
        public string AdminName { get; set; }

        [Required(ErrorMessage = "Parola boş bırakılamaz!")]
        [DisplayName("Parola")]
        [DataType(DataType.Password)]
        public string AdminPassword { get; set; }
    }
}
