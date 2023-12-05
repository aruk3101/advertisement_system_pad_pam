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
    [Table("users_courses")]
    public class Course : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }

        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Organizator jest wymagany")]
        public string Host { get; set; }

        [Required(ErrorMessage = "Data ukończenia jest wymagan")]
        public DateTime Date { get; set; }
    }
}
