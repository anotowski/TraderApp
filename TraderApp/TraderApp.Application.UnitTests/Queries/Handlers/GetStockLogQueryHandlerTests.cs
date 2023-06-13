using NUnit.Framework;
using AutoFixture;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using TraderApp.Application.Parsers;
using TraderApp.Application.Queries;
using TraderApp.Application.Queries.Handlers;
using TraderApp.Domain.Models.StockLog;
using TraderApp.Domain.Repositories;
using TraderApp.UnitTests.Helpers;

namespace TraderApp.Application.UnitTests.Queries.Handlers;

public class GetStockLogQueryHandlerTests
{
    private IFixture _fixture;
    private IDateTimeParser _dateTimeParser;
    private IGetStockLogQueryHandler _sut;
    private CancellationToken _cancellationToken;
    private IStockLogRepository _stockLogRepository;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture();
        _dateTimeParser = _fixture.FreezeSubstitute<IDateTimeParser>();
        _stockLogRepository = _fixture.FreezeSubstitute<IStockLogRepository>();
        _sut = _fixture.Create<GetStockLogQueryHandler>();

        _cancellationToken = _fixture.Create<CancellationToken>();
    }

    [Test]
    public async Task HandleAsync_GivenQuery_AssertDateTimeParserIsUsed()
    {
        // Arrange
        var query = _fixture.Create<GetStockLogQuery>();
        _dateTimeParser.Parse(Arg.Any<string>())
            .Returns(DateTime.UtcNow);
        // Act
        _ = await _sut.HandleAsync(query, _cancellationToken);
        
        // Assert
        _dateTimeParser.Received(1).Parse(query.From);
        _dateTimeParser.Received(1).Parse(query.To);
    }

    [Test]
    public async Task HandleAsync_GivenHappyPath_AssertResultIsCorrectlyMapped()
    {
        // Arrange
        var query = _fixture.Create<GetStockLogQuery>();

        var expectedDate = DateTime.UtcNow;
        _dateTimeParser.Parse(Arg.Any<string>())
            .Returns(expectedDate);

        var expectedStockLog = _fixture.CreateMany<StockLog>(1)
            .ToArray();
        
        _stockLogRepository.GetWithinDateAsync(expectedDate, expectedDate, _cancellationToken)
            .Returns(expectedStockLog);
        
        // Act
        var result = (await _sut.HandleAsync(query, _cancellationToken)).ToList();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Single().Id, Is.EqualTo(expectedStockLog.Single().Id));
            Assert.That(result.Single().EndpointName, Is.EqualTo(expectedStockLog.Single().EndpointName));
            Assert.That(result.Single().AttemptResult, Is.EqualTo(expectedStockLog.Single().AttemptResult));
            Assert.That(result.Single().AttemptDate, Is.EqualTo(expectedStockLog.Single().AttemptDate));
            Assert.That(result.Single().AttemptResultMessage, Is.EqualTo(expectedStockLog.Single().AttemptResultMessage));
        });
    }
}

