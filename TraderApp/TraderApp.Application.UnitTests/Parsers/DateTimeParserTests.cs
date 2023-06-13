using System.Globalization;
using AutoFixture;
using NUnit.Framework;
using TraderApp.Application.Parsers;

namespace TraderApp.Application.UnitTests.Parsers;


public class DateTimeParserTests
{
    private IFixture _fixture;
    private IDateTimeParser _sut;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _sut = _fixture.Create<DateTimeParser>();
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase(null)]
    public void Parse_GivenEmptyString_AssertExceptionIsThrown(string date)
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Parse(date));
    }

    [TestCase("2023-02-02-22-21-22")]
    [TestCase("1990-12-24-01-10-00")]
    public void Parse_GivenValidDate_AssertResultIsCorrect(string date)
    {
        // Arrange
        var expectedDate = DateTime.ParseExact(date, DateTimeParser.DateTimeFormat, CultureInfo.InvariantCulture);
        
        // Act
        var result = _sut.Parse(date);
        
        // Assert
        Assert.That(result, Is.EqualTo(expectedDate));
    }

    [TestCase("22222")]
    [TestCase("xyz")]
    [TestCase("02/05/2021")]
    [TestCase("22/23/2021-22-22-22")]
    [TestCase("2021-33-05-22-22-22-22")]
    [TestCase("0-01-05-22-22-22-22")]
    [TestCase("2000-01-05-22-22-22-77")]
    [TestCase("2000-01-05-22-22-88-22")]
    [TestCase("2000-01-05-22-300-08-22")]
    public void Parse_GivenInvalidDate_AssertThrowsException(string date)
    {
        // Arrange
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => _sut.Parse(date));

    }

    [Test]
    public void Parse_GivenInvalidDate_AssertThrownExceptionMessageIsCorrect()
    {
        // Arrange
        var invalidDate = _fixture.Create<string>();
        var expectedMessage = $"Incorrect format for: 'date': '{invalidDate}'.";
        
        // Act
        var exception = Assert.Throws<ArgumentException>(() => _sut.Parse(invalidDate));
        
        // Assert
        Assert.That(exception.Message, Contains.Substring(expectedMessage));
    }
}
