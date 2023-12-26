using Projekt.Models.Common.Enumerated;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataAnnotations = System.ComponentModel.DataAnnotations;
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
        [MinLength(3, ErrorMessage = "Imie musi zawierać conajmniej 3 znaki")]
        [DataAnnotations.MaxLength(100, ErrorMessage = "Imie musi zawierać maksymalnie 100 znaków")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Imie może zawierać tylko litery unicode oraz białe znaki")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Imie nie może być puste")]
        public string FirstName { get; set; }

        [SQLite.MaxLength(255)]
        [MinLength(3, ErrorMessage = "Nazwisko musi zawierać conajmniej 3 znaki")]
        [DataAnnotations.MaxLength(100, ErrorMessage = "Nazwisko musi zawierać maksymalnie 100 znaków")]
        [RegularExpression(@"^[\p{L}\s]*$", ErrorMessage = "Nazwisko może zawierać tylko litery unicode oraz białe znaki")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwisko nie może być puste")]
        public string LastName { get; set; }

        [Unique, SQLite.MaxLength(255)]
        [EmailAddress(ErrorMessage = "Email musi być poprawnie sformatowanym adresem e-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email nie może być pusty")]
        public string Email { get; set; }

        [SQLite.MaxLength(100)]
        [MinLength(8, ErrorMessage = "Haśło musi zawierać conajmniej 8 znaków")]
        [DataAnnotations.MaxLength(100, ErrorMessage = "Hasło może zawierać maksymalnie 100 znaków")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Hasło nie może być puste")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "Hasło musi zawierać conajmniej 1 małą i dużą literę")]
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
        public string? ProfilePictureSource { get; set; }

        [SQLite.MaxLength(255)]
        public string CurrentJob { get; set; }

        [SQLite.MaxLength(1023)]
        public string CurrentJobDescription { get; set; }

        [SQLite.MaxLength(1023)]
        public string CareerSummary { get; set; }

        public Role Role { get; set; } = Role.USER;

        [Ignore]
        public List<Course> Courses { get; set; }

        [Ignore]
        public List<Education> Educations { get; set; }

        [Ignore]
        public List<Experience> Experiences { get; set; }

        [Ignore]
        public List<Language> Languages { get; set; }

        [Ignore]
        public List<Link> Links { get; set; }

        [Ignore]
        public List<Skill> Skills { get; set; }

    }
}
