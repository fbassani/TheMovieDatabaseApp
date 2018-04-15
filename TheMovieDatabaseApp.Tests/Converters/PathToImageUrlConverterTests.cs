using System;
using NUnit.Framework;
using TheMovieDatabaseApp.Converters;

namespace TheMovieDatabaseApp.Tests.Converters
{
    public class PathToImageUrlConverterTests
    {
        private PathToImageUrlConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new PathToImageUrlConverter();
        }

        [Test]
        public void Convert_WithPath_ShouldConvertToUrl()
        {
            var path = "/image.jpg";
            var converted = _converter.Convert(path, null, null, null);
            var expected = $"{Settings.ImagesBaseUrl}{path}";
            Assert.AreEqual(expected, converted);
        }

        [Test]
        public void Convert_WithoutPath_ShouldReturnNull()
        {
            Assert.IsNull(_converter.Convert(new object(), null, null, null));
        }

        [Test]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(null, null, null, null));
        }
    }
}