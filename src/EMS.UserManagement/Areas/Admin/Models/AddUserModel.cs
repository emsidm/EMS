using System;
using System.ComponentModel.DataAnnotations;
using EMS.CachingDbContext.Models;

namespace EMS.UserManagement.Areas.Admin.Models
{
    public class AddUserModel
    {
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)] public string EmailAddress { get; set; }
        [DataType(DataType.PhoneNumber)] public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string ManagerId { get; set; }
    }
}