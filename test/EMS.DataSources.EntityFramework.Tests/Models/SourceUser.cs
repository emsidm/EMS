using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Contracts.DataAccess;

namespace EMS.DataSources.EntityFramework.Tests.Models
{
    public class SourceUser
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }
        
        public bool Provisioned { get; set; }

        public override string ToString() => $"{UserName} <{EmailAddress}>";
    }
}