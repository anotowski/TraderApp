using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraderApp.Application.Commands;
using TraderApp.Application.Commands.Handlers;

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

    public StockController( IConfiguration configuration,
        IUploadTradeSiteCommandHandler uploadTradeSiteCommandHandler)
    {
        _configuration = configuration;
        _uploadTradeSiteCommandHandler = uploadTradeSiteCommandHandler;
    }
    
    /// <summary>
    /// Get list of stock data saved in given time period
    /// </summary>
    /// <param name="from">Starting date</param>
    /// <param name="to">End date</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Get([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        return Ok();
    }


    /// <summary>
    /// Get detailed stock data for given id
    /// </summary>
    /// <param name="id">Unique ID of a stock</param>
    /// <returns>Detailed information about stock</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Get([FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Upload()
    {
        var siteUrl = _configuration["TradeSiteRedditEndpoint"];
        var result = await _uploadTradeSiteCommandHandler.HandleAsync(new UploadTradeSiteCommand(){EndpointRoute = siteUrl}, HttpContext.RequestAborted);

        return Ok(result);
    }
}
