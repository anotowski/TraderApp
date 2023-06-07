using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraderApp.Controllers;

// <summary>
/// Controller that handles retrieval of stock data
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class StockController : ControllerBase
{
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
}
