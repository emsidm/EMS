using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EMS.Contracts.DataAccess;

namespace EMS.CachingDbContext.Models
{
    public class User : IEntityBase
    {
        string IEntityBase.Id => Id.ToString();
        [Key] public Guid Id { get; set; }

        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)] public string EmailAddress { get; set; }

        public string FullName { get; set; }

        [InverseProperty(nameof(Manages))] public User Manager { get; set; }

        [InverseProperty(nameof(Manager))] public List<User> Manages { get; set; }

        [DataType(DataType.PhoneNumber)] public string PhoneNumber { get; set; }
    }
}