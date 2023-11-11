using Projekt.Models.Common.Enumerated;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("users")]
    public class User : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [SQLite.MaxLength(255)]
        public string FirstName { get; set; }

        [SQLite.MaxLength(255)]
        public string LastName { get; set; }

        [Unique, SQLite.MaxLength(255)]
        public string Email { get; set; }

        [SQLite.MaxLength(100)]
        public string PasswordHash { get; set; }

        public DateTime? BirthDate { get; set; }

        [SQLite.MaxLength(9)]
        public string? PhoneNumber { get; set; }

        [SQLite.MaxLength(255)]
        public string? City { get; set; }

        [SQLite.MaxLength(255)]
        public string? PostalName { get; set; }

        [SQLite.MaxLength(6)]
        public string? PostalCode { get; set; }

        [SQLite.MaxLength(255)]
        public string? Street { get; set; }

        [SQLite.MaxLength(5)]
        public string? StreetNumber { get; set; }

        [SQLite.MaxLength(5)]
        public string? ApartmentNumber { get; set; }

        [SQLite.MaxLength(255)]
        public string? Country { get; set; }

        [SQLite.MaxLength(255)]
        public string ProfilePictureSource { get; set; }

        public Role Role { get; set; } = Role.USER;
    }
}
