using Projekt.Models.Common.Enumerated;
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
    [Table("users_languages")]
    public class Language : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }

        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Język jest wymagany")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Poziom jest wymagany")]
        public LanguageLevel Level { get; set; }
    }
}
