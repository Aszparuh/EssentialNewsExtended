using EssentialNewsMvc.Data;
using NUnit.Framework;
using System.Data.Entity;

namespace EssentialNewsMvc.Tests.IntegrationTests
{
    [SetUpFixture]
    public class TestInitializer
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Database.SetInitializer(new DatabaseSeedingInitializer());
        }

        public class DatabaseSeedingInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
        }
    }
}
