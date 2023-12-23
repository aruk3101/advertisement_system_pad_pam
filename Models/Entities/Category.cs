using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("categories")]
    public class Category : ValidatableEntity, IEquatable<Category>
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(Category? other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}
