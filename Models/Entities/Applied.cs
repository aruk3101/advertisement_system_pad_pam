using Projekt.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("advertisemenst_applied")]
    public class Applied : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AdvertisementId { get; set; }
    }
}
