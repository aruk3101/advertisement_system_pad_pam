﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("advertisements_responsibilities")]
    public class Responsibility : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AdvertisementId { get; set; }

        public string Name { get; set; }
    }
}
