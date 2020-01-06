using com.b_velop.Slipways.Data.Extensions;
using com.b_velop.Slipways.Data.Models;
using NUnit.Framework;
using System;

namespace Slipways.Data.Tests
{
    [TestFixture]
    public class SlipwayWrapperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SlipwayWrapper_ToDto_ExtrasIsEmptyWhenNoExtrasInClass()
        {
            // Arrange
            var slipway = new Slipway
            {
                Id = Guid.Parse("F2638A1A-9F5C-4858-BC04-786149E9ECFF"),
                Name = "Test",
                City = "Köln",
                WaterFk = Guid.Parse("5790B3B2-A002-45C3-821D-6D8EC194397E")
            };

            // Act
            var actual = slipway.ToDto().Extras;

            // Assert
            Assert.IsEmpty(actual);
        }
    }
}