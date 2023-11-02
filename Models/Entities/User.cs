using Projekt.Models.Common.Enumerated;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [Unique, MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string PasswordHash { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(9)]
        public string? PhoneNumber { get; set; }
        [MaxLength(255)]
        public string? City { get; set; }
        [MaxLength(255)]
        public string? PostalName { get; set; }
        [MaxLength(6)]
        public string? PostalCode { get; set; }
        [MaxLength(255)]
        public string? Street { get; set; }
        [MaxLength(5)]
        public string? StreetNumber { get; set; }
        [MaxLength(5)]
        public string? ApartmentNumber { get; set; }
        [MaxLength(255)]
        public string? Country { get; set; }
        [MaxLength(255)]
        public string ProfilePictureSource { get; set; }
        public Role Role { get; set; } = Role.USER;
    }
}
