using HealthChecker.Business.Utilities;
using Xunit;

namespace HealthChecker.UnitTests.BusinessTests
{
    public class UtilityTests
    {
        [Theory]
        [InlineData("http://google.com/", true)]
        [InlineData("http://www.google.com/", true)]
        [InlineData("http://google.com/asdf", true)]
        [InlineData("http://google.com/asdf/qwer", true)]
        [InlineData("http://www.google.com/asdf", true)]
        [InlineData("http://www.google.com/asdf/qwer", true)]
        [InlineData("http://google.com/asdf/qwer?q=1", true)]
        [InlineData("http://www.google.com/asdf/qwer?q=1", true)]
        [InlineData("http://google.com/asdf/qwer?q=1&zxcv=zxvzxcv", true)]
        [InlineData("http://www.google.com/asdf/qwer?q=1&zxcv=zxvzxcv", true)]
        [InlineData("https://google.com/", true)]
        [InlineData("https://www.google.com/", true)]
        [InlineData("https://google.com/asdf", true)]
        [InlineData("https://google.com/asdf/qwer", true)]
        [InlineData("https://www.google.com/asdf", true)]
        [InlineData("https://www.google.com/asdf/qwer", true)]
        [InlineData("https://google.com/asdf/qwer?q=1", true)]
        [InlineData("https://www.google.com/asdf/qwer?q=1", true)]
        [InlineData("https://google.com/asdf/qwer?q=1&zxcv=zxvzxcv", true)]
        [InlineData("https://www.google.com/asdf/qwer?q=1&zxcv=zxvzxcv", true)]
        [InlineData("http://mail.google.com/", true)]
        [InlineData("https://mail.google.com/", true)]
        [InlineData("http://mail.google.com/?", true)]
        [InlineData("https://mail.google.com/?", true)]
        public void ValidUrlAttribute_ShouldValidateUrlStrings(string url, bool expected)
        {
            ValidUrlAttribute validUrlAttribute = new ValidUrlAttribute();

            Assert.Equal(expected, validUrlAttribute.IsValid(url));
        }

        [Theory]
        [InlineData("google.com", false)]
        [InlineData("www.google.com", false)]
        [InlineData("google.com/", false)]
        [InlineData("www.google.com/", false)]
        [InlineData("google.com/asdf", false)]
        [InlineData("google.com/asdf/qwer", false)]
        [InlineData("www.google.com/asdf", false)]
        [InlineData("www.google.com/asdf/qwer", false)]
        [InlineData("google.com/asdf/qwer?q=1", false)]
        [InlineData("www.google.com/asdf/qwer?q=1", false)]
        [InlineData("google.com/asdf/qwer?q=1&zxcv=zxvzxcv", false)]
        [InlineData("www.google.com/asdf/qwer?q=1&zxcv=zxvzxcv", false)]
        [InlineData("mail.google.com/", false)]
        [InlineData("mail.google.com/?", false)]
        [InlineData("ftp://google.com", false)]
        public void ValidUrlAttribute_ShouldNotValidateUrlStrings(string url, bool expected)
        {
            ValidUrlAttribute validUrlAttribute = new ValidUrlAttribute();

            Assert.Equal(expected, validUrlAttribute.IsValid(url));
        }
    }
}