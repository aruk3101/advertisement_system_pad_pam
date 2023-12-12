using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAnnotations = System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("users_skills")]
    public class Skill : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }

        [SQLite.MaxLength(255)]
        [DataAnnotations.MaxLength(255)]
        [MinLength(3)]
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }
    }
}
