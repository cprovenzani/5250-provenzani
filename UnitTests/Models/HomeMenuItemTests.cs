using NUnit.Framework;
using Mine.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class HomeMenuItemTests
    {
        [Test]
        public void HomeMenuItemTests_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new HomeMenuItem();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void HomeMenuItemTests_Set_Get_Valid_Default_Should_Pass()
        {
            // Arrange
            var type = MenuItemType.About;

            // Act
            var result = new HomeMenuItem();
            result.Id = type;
            result.Title = "Title";

            // Reset

            //Assert
            Assert.AreEqual(type, result.Id);         
            Assert.AreEqual("Title", result.Title);         
        }
    }
}
