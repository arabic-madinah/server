using MyArabic.WebApi.Validators;

namespace MyArabic.Tests.Validators;

public class SlugValidatorTests
{
    [Theory]
    [InlineData("valid-slug")]
    [InlineData("slug")]
    [InlineData("a1-b2-c3")]
    [InlineData("abc123")]
    [InlineData("a")]
    public void Validate_ValidSlugs_ReturnsTrue(string slug)
    {
        Assert.True(SlugValidator.Validate(slug));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("InvalidSlug")]
    [InlineData("invalid_slug")]
    [InlineData("invalid--slug")]
    [InlineData("-start")]
    [InlineData("end-")]
    [InlineData("multiple--dashes")]
    [InlineData("UPPERCASE")]
    [InlineData("with space")]
    [InlineData("special!char")]
    [InlineData("route1/route2")]
    public void Validate_InvalidSlugs_ReturnsFalse(string slug)
    {
        Assert.False(SlugValidator.Validate(slug));
    }
}
