using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProCards.DAL.Interfaces;
using ProCards.Web.Filters;

namespace ProCards.Web.Controllers.ApiControllers;

[ApiController]
[Route("categories")]
public class CategoryController: ControllerBase
{
    [HttpGet]
    [CategoriesActionFilter]
    public async Task<IActionResult> GetUserCategories([FromServices] ICategoryRepository categoryRepository, [FromQuery] int firstId)
    {
        var categories = await categoryRepository.GetNineUserCategoriesAsync(firstId);
        if (categories.Item2)
            HttpContext.Response.Headers["is-last"] = "true";
        return Ok(categories.Item1);
    }
}