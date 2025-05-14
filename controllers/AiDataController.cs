using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class AiDataController : ControllerBase
{
    private readonly StocksDbContext _context;
    public AiDataController(StocksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Exchange>>> GetAiData()
    {
        return Ok(await _context.AiData.ToListAsync());
    }


}