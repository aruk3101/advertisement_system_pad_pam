using Projekt.Models.Common.Enumerated;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAnnotations = System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("users_education")]
    public class Education : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Nazwa szkoły jest wymagana")]
        public string SchoolName { get; set; }
        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Adres szkoły jest wymagana")]
        public string SchoolLocation { get; set; }
        [Required(ErrorMessage = "Poziom edukacji jest wymagany")]
        public EducationLevel Level { get; set; }
        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Specjalizacja jest wymagana")]
        public string Specialization { get; set; }
        [Required(ErrorMessage = "Początek okresu jest wymagany")]
        public DateTime PeriodStart { get; set; }
        [Required(ErrorMessage = "Koniec okresu jest wymagany")]
        public DateTime PeriodEnd { get; set; }
        
    }
}
