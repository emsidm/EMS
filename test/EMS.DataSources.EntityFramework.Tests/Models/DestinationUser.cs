using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Contracts.DataAccess;

namespace EMS.DataSources.EntityFramework.Tests.Models
{
    public class DestinationUser
    {
        [Key] public Guid Id { get; set; }
        
        [Required]
        public string SamAccountName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }
    }
}