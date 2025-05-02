// Models/ClassInformationModel.cs
using System.ComponentModel.DataAnnotations;

namespace Ceng382_25_26_202011037.Models
{
    public class ClassInformationModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Sınıf adı zorunlu.")]
        public string? ClassName { get; set; }

        [Required(ErrorMessage = "Öğrenci sayısı zorunlu.")]
        [Range(1, 1000, ErrorMessage = "Öğrenci sayısı 1–1000 aralığında olmalı.")]
        public int? StudentCount { get; set; }

        [Required(ErrorMessage = "Açıklama zorunlu.")]
        public string? Description { get; set; }
    }
}
