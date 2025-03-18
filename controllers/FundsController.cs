using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class FundsController : ControllerBase
{
    private readonly StocksDbContext _context;
    public FundsController(StocksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Fund>>> GetFunds()
    {
        return Ok(await _context.StockDataFunds.ToListAsync());
    }

    [HttpGet("{symbol}")]
    public async Task<ActionResult<Fund>> GetFundBySymbol(string symbol)
    {
        var exchange = await _context.StockDataFunds
            .FirstOrDefaultAsync(e => e.Sembol == symbol);
        if (exchange == null)
        {
            return NotFound();
        }
        return Ok(exchange);
    }

}