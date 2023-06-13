using NUnit.Framework;
using AutoFixture;
using TraderApp.Application.Queries;

namespace TraderApp.Application.UnitTests.Queries;

public class GetStockByIdQueryTests
{
    private IFixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
    }

    [Test]
    public void GetStockByIdQuery_GivenEmptyId_AssertThrowsException()
    {
        // Arrange
        var emptyGuid = Guid.Empty;
        
        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new GetStockByIdQuery
        {
            Id = emptyGuid
        });
    }
}

