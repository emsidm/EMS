using System;
using System.Linq;
using EMS.Contracts.DataAccess;
using EMS.DataSources.EntityFramework.Tests.Contexts;
using EMS.DataSources.EntityFramework.Tests.Helpers;
using EMS.DataSources.EntityFramework.Tests.Models;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace EMS.DataSources.EntityFramework.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        private SqliteConnection _dstConnection;

        [SetUp]
        public void Setup()
        {
            DbConnectionHelpers.SeedContext<DestinationDbContext>(out _dstConnection);
        }
        
        [TearDown]
        public void TearDown()
        {
            _dstConnection.Close();
        }
        
        [Test]
        public void InexistentEntity_Exists_ReturnsFalse()
        {
            var dbContext = new DestinationDbContext(_dstConnection);
            {
                var dstEntity = new DestinationUser
                {
                    EmailAddress = "pesho@pesho.org",
                    SamAccountName = "pesho"
                };

                Assert.That(dbContext.EntityExists(dstEntity), Is.False);
            }
        }
        
        [Test]
        public void FakeEntity_Exists_ReturnsFalse()
        {
            var dbContext = new DestinationDbContext(_dstConnection);
            {
                var dstEntity = new DestinationUser
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = "pesho@pesho.org",
                    SamAccountName = "pesho"
                };

                Assert.That(dbContext.EntityExists(dstEntity), Is.False);
            }
        }
        
                
        [Test]
        public void RealEntity_Exists_ReturnsTrue()
        {
            var dbContext = new DestinationDbContext(_dstConnection);
            {
                var dstEntity = new DestinationUser
                {
                    EmailAddress = "pesho@pesho.org",
                    SamAccountName = "pesho"
                };

                dbContext.Add(dstEntity);
                dbContext.SaveChanges();

                Assert.That(dbContext.EntityExists(dstEntity), Is.True);
            }
        }
    }
}