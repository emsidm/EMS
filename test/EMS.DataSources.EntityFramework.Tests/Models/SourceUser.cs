using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Contracts.DataAccess;

namespace EMS.DataSources.EntityFramework.Tests.Models
{
    public class SourceUser : IEntityBase
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }
        
        public bool Provisioned { get; set; }

        [NotMapped] public string Id => UserId.ToString();

        public override string ToString() => $"{UserName} <{EmailAddress}>";
    }
}