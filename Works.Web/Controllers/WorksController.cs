using Works.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Works.Entities;

namespace Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с работами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WorksController : ControllerBase
    {
        private readonly WorksContext context;

        /// <summary>
        /// Конструктор
        /// </summary>
        public WorksController(WorksContext context) {
            this.context = context;
        }

        /// <summary>
        /// Получает список всех работ
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Work>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var items = await context.Set<Work>()
                .AsNoTracking()
                .AsQueryable()
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return Ok(items);
        }
    }
}