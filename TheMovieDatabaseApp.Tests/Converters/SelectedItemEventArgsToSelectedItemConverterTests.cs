using System;
using NUnit.Framework;
using TheMovieDatabaseApp.Converters;
using Xamarin.Forms;

namespace TheMovieDatabaseApp.Tests.Converters
{
    public class SelectedItemEventArgsToSelectedItemConverterTests
    {
        private SelectedItemEventArgsToSelectedItemConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new SelectedItemEventArgsToSelectedItemConverter();
        }

        [Test]
        public void Convert_WithSelectedItemChangedEventArgs_ShouldReturnSelectedItem()
        {
            var selected = new Object();
            var args = new SelectedItemChangedEventArgs(selected);
            var converted = _converter.Convert(args, null, null, null);
            Assert.AreSame(selected, converted);
        }

        [Test]
        public void Convert_WithOtherValue_ShouldReturnNull()
        {
            var converted = _converter.Convert("test", null, null, null);
            Assert.IsNull(converted);
        }

        [Test]
        public void ConvertBack_ShouldThrowNotImplementedException()
        {
            Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(new Object(), null, null, null));
        }
    }
}