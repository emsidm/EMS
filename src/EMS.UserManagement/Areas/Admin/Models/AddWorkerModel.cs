using System.ComponentModel.DataAnnotations;

namespace EMS.UserManagement.Areas.Admin.Models
{
    public class AddWorkerModel
    {
        [Required] public string Name { get; set; }
    }
}