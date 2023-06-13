using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraderApp.Application.Commands;
using TraderApp.Application.Commands.Handlers;
using TraderApp.Application.Queries;
using TraderApp.Application.Queries.Handlers;

namespace TraderApp.Controllers;

/// <summary>
/// Controller that handles retrieval of stock data
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class StockController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUploadTradeSiteCommandHandler _uploadTradeSiteCommandHandler;
    private readonly IGetStockByIdQueryHandler _getStockByIdQueryHandler;
    private readonly IGetStockLogQueryHandler _getStockLogQueryHandler;

    /// <summary>
    /// Controller used for stock operations
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="uploadTradeSiteCommandHandler"></param>
    /// <param name="getStockByIdQueryHandler"></param>
    public StockController(IConfiguration configuration,
        IUploadTradeSiteCommandHandler uploadTradeSiteCommandHandler,
        IGetStockByIdQueryHandler getStockByIdQueryHandler,
        IGetStockLogQueryHandler getStockLogQueryHandler)
    {
        _configuration = configuration;
        _uploadTradeSiteCommandHandler = uploadTradeSiteCommandHandler;
        _getStockByIdQueryHandler = getStockByIdQueryHandler;
        _getStockLogQueryHandler = getStockLogQueryHandler;
    }

    /// <summary>
    /// Get list of stock data saved in given time period
    /// </summary>
    /// <param name="from">Starting date</param>
    /// <param name="to">End date</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetStockLogResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetStockLogResult>>> Get([FromQuery] GetStockLogQuery query)
    {
        var result = await _getStockLogQueryHandler.HandleAsync(query,
            HttpContext.RequestAborted);

        return Ok(result);
    }


    /// <summary>
    /// Get detailed stock data for given id
    /// </summary>
    /// <param name="id">Unique ID of a stock</param>
    /// <returns>Detailed information about stock</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetStockByIdResult>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<GetStockByIdResult>>> GetById([FromRoute] Guid id)
    {
        var result = await _getStockByIdQueryHandler.HandleAsync(
            new GetStockByIdQuery() { Id = id },
            HttpContext.RequestAborted);

        return Ok(result);
    }

    /// <summary>
    /// Gets stock data and saves result
    /// </summary>
    /// <returns>Guid of a new entity</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Guid>> Upload()
    {
        var siteUrl = _configuration["TradeSiteRedditEndpoint"];
        var result =
            await _uploadTradeSiteCommandHandler.HandleAsync(new UploadTradeSiteCommand() { EndpointRoute = siteUrl },
                HttpContext.RequestAborted);

        return CreatedAtAction(nameof(GetById), new { id = result }, new { Id = result });
    }
}
