using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ExchangesController : ControllerBase
{
    private readonly StocksDbContext _context;
    public ExchangesController(StocksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Exchange>>> GetExchanges()
    {
        return Ok(await _context.StockDataExchanges.ToListAsync());
    }

    [HttpGet("{symbol}")]
    public async Task<ActionResult<Exchange>> GetExchangeBySymbol(string symbol)
    {
        var exchange = await _context.StockDataExchanges
            .FirstOrDefaultAsync(e => e.Sembol == symbol);
        if (exchange == null)
        {
            return NotFound();
        }
        return Ok(exchange);
    }

}