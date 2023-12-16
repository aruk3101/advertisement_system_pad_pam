using Projekt.Models.Common.Enumerated;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Entities
{
    #nullable enable
    [Table("advertisements")]
    public class Advertisement : ValidatableEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string PositionLevel { get; set; }
        public ContractType ContractType { get; set; }
        public WorkingTimeType WorkingTimeType { get; set; }
        public JobType JobType { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public string WorkingDays { get; set; }
        public string WorkingHours { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Opportunities { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }

        [Ignore]
        public Category Category { get; set; }
    }
}
