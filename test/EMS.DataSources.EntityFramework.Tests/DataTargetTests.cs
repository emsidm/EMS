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
    public class DataTargetTests
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
        public async Task InitialProvisioning_CreatesUser()
        {
            using (var destination =
                new EfDataContext<DestinationDbContext>(new DestinationDbContext(_dstConnection)) as IDataTarget)
            {
                var dstEntity = new DestinationUser
                {
                    EmailAddress = "pesho@pesho.org",
                    SamAccountName = "pesho"
                };

                AsyncTestDelegate @delegate = async () =>
                {
                    Assert.That((await destination.GetProvisioningStatusAsync(dstEntity)).State,
                        Is.EqualTo(ProvisioningState.Inexistent));
                    Assert.That((await destination.ProvisionAsync(dstEntity)).State,
                        Is.EqualTo(ProvisioningState.Created));
                    Assert.That((await destination.GetProvisioningStatusAsync(dstEntity)).State,
                        Is.EqualTo(ProvisioningState.Unmodified));
                };


                Assert.Multiple(@delegate);
            }
        }

        [Test]
        public async Task Preprovisioned_UpdatesUser()
        {
            using (var destination =
                new EfDataContext<DestinationDbContext>(new DestinationDbContext(_dstConnection)) as IDataTarget)
            {
                var dstEntity = new DestinationUser
                {
                    EmailAddress = "gosho@pesho.org",
                    SamAccountName = "gosho"
                };
                await destination.ProvisionAsync(dstEntity);
                dstEntity.EmailAddress = "pesho@pesho.org";
                dstEntity.SamAccountName = "pesho";

                var changedEntity = dstEntity;
                var provisioningStatus = await destination.ProvisionAsync(changedEntity);

                Assert.Multiple(
                    () =>
                    {
                        Assert.That(provisioningStatus.State,
                            Is.EqualTo(ProvisioningState.Modified));
                        Assert.That(provisioningStatus.Entities.First().EmailAddress,
                            Is.EqualTo(dstEntity.EmailAddress));
                    }
                );
            }
        }

        [Test]
        public async Task Unchanged_ReturnsUnmodified()
        {
            using (var destination =
                new EfDataContext<DestinationDbContext>(new DestinationDbContext(_dstConnection)) as IDataTarget)
            {
                var dstEntity = new DestinationUser
                {
                    EmailAddress = "pesho@pesho.org",
                    SamAccountName = "pesho"
                };
                await destination.ProvisionAsync(dstEntity);

                var changedEntity = dstEntity;
                var provisioningStatus = await destination.ProvisionAsync(changedEntity);

                Assert.That(provisioningStatus.State, Is.EqualTo(ProvisioningState.Unmodified));
            }
        }
    }
}