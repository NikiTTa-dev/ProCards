using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProCards.DAL.Interfaces;

namespace ProCards.Web.Controllers.ApiControllers;

[ApiController]
[Route("categories")]
public class CategoryController: ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserCategories([FromServices] ICategoryRepository categoryRepository, [FromQuery] int firstId)
    {
        return Ok(await categoryRepository.GetNineUserCategories(firstId));
    }
}