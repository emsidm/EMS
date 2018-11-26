using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Contracts.DataAccess;

namespace EMS.DataSources.EntityFramework.Tests.Models
{
    public class DestinationUser : IEntityBase
    {
        [Key] public Guid ObjectGuid { get; set; }
        
        [Required]
        public string SamAccountName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }

        [NotMapped] public string Id => ObjectGuid.ToString();
    }
}