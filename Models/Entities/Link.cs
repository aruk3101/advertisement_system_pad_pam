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
    [Table("users_links")]
    public class Link : ValidatableEntity
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }

        [SQLite.MaxLength(255)]
        [Required(ErrorMessage = "Nazwa linku jest wymagana")]
        public string Name { get; set; }

        [SQLite.MaxLength(1000)]
        [Required(ErrorMessage = "Adres linku jest wymagany")]
        public string Hyperlink { get; set; }
    }
}
