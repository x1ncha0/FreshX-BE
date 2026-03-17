using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(IMenuService menuService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? searchKey, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await menuService.GetMenusAsync(searchKey));
    }

    [HttpGet("{menuId:int}")]
    public async Task<IActionResult> GetById(int menuId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Ok(await menuService.GetMenuByIdAsync(menuId));
    }
}
