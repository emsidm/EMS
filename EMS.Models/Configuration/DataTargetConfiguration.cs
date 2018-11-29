using System;
using System.ComponentModel.DataAnnotations;
using EMS.Contracts.DataAccess;

namespace EMS.Models.Configuration
{
    public class DataTargetConfiguration
    {
        [Key] public Guid Id { get; set; }

        [Required] public string Name { get; set; }
        public string ContextType { get; set; }
        [Required] public string Connector { get; set; }
        public string ConnectionString { get; set; }
    }
}