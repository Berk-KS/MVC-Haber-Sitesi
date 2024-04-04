using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberApi.Models
{
    public class Haber
    {
        [Key]
        public int HaberId { get; set; }

        [Required(ErrorMessage = "Haber başlığı boş bırakılamaz!")]
        [DisplayName("Haber Başlığı")]
        public string HaberBaslik { get; set; }

        [Required(ErrorMessage = "Haber içeriği boş bırakılamaz!")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Haber İçeriği")]
        public string HaberIcerik { get; set; }

        [DisplayName("Haber Görseli")]
        public string? HaberGorsel { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:DD.MM.YYYY}")]
        public DateTime dateTime { get; set; } = DateTime.Now;

        public string? Kategori { get; set; }
    }
}
