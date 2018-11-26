using System.ComponentModel.DataAnnotations;

namespace EMS.Contracts.DataAccess
{
    public interface IEntityBase
    {
        [Key]
        string Id { get; }
    }
}