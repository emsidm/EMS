using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using EMS.Contracts.DataAccess;
using EMS.DataSources.EntityFramework.Tests.Contexts;
using EMS.DataSources.EntityFramework.Tests.Helpers;
using EMS.DataSources.EntityFramework.Tests.Models;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace EMS.DataSources.EntityFramework.Tests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public class DataSourceTests
    {
        private SqliteConnection _srcConnection;

        [SetUp]
        public void Setup()
        {
            DbConnectionHelpers.SeedContext<SourceDbContext>(out _srcConnection);
        }

        [TearDown]
        public void TearDown()
        {
            _srcConnection.Close();
        }

        [Test]
        public void UserAdded_AppearsInContext()
        {
            SourceUser newUser;
            var srcUser = new SourceUser
            {
                EmailAddress = "pesho@pehso.org",
                UserName = "pesho"
            };

            using (var efContext = new SourceDbContext(_srcConnection))
            {
                efContext.Users.Add(srcUser);
                efContext.SaveChanges();
            }

            using (var source =
                new EfDataContext<SourceDbContext>(new SourceDbContext(_srcConnection)) as IDataSource)
            {
                newUser = source.Entities<SourceUser>().Single(x => x.EmailAddress == srcUser.EmailAddress);
            }

            Assert.Multiple(() =>
            {
                Assert.That(newUser.Id, Is.EqualTo(srcUser.Id));
                Assert.That(newUser.EmailAddress, Is.EqualTo(srcUser.EmailAddress));
            });
        }

        [Test]
        public void UserNotInDb_ReturnsEmpty()
        {
            bool exists;
            using (var source =
                new EfDataContext<SourceDbContext>(new SourceDbContext(_srcConnection)) as IDataSource)
            {
                exists = source.Entities<SourceUser>().Any(x => x.EmailAddress == "pesho@pesho.org");
            }

            Assert.Multiple(() =>
            {
                Assert.False(exists);
            });
        }
    }
}