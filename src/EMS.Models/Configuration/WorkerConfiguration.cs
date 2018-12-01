using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EMS.Contracts.DataAccess;

namespace EMS.Models.Configuration
{
    public class WorkerConfiguration
    {
        [Key] public Guid Id { get; set; }

        [Required] public string WorkerName { get; set; }
        public string ApiKey { get; set; }
        
        public IEnumerable<DataSourceConfiguration> Sources { get; set; }
        public IEnumerable<DataTargetConfiguration> Targets { get; set; }
        public IEnumerable<DataContextConfiguration> Contexts { get; set; }
    }
}