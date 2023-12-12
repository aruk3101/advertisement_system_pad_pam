using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("users_experiences")]
    public class Experience : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Stanowisko jest wymagane")]
        public string Position { get; set; }

        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Nazwa firmy jest wymagana")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Adres firmy jest wymagany")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Początek okresu jest wymagany")]
        public DateTime PeriodStart { get; set; }

        [Required(ErrorMessage = "Koniec okresu jest wymagany")]
        public DateTime PeriodEnd { get; set; }

        [SQLite.MaxLength(3000)]
        public string Duties { get; set; }
    }
}
