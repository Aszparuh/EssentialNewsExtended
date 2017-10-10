using EssentialNewsMvc.Web.Controllers;
using NUnit.Framework;

namespace EssentialNewsMvc.Tests.UnitTests.HomeControllerTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ReturnInstance()
        {
            var controller = new HomeController();

            Assert.That(controller, Is.InstanceOf<HomeController>(), "Instance");
        }
    }
}
