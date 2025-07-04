using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Works.Web.Models;
using Works.Services.Contracts.Models;
using Works.Services.Contracts;
using Works.Web.Models.Exceptions;

namespace Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с работами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WorksController : ControllerBase
    {
        private readonly IWorksServices service;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// Конструктор
        /// </summary>
        public WorksController(IWorksServices service, IValidateService validateService, IMapper mapper)
        {
            this.service = service;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает список всех товаров
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorksApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await service.GetAll(cancellationToken);

            return Ok(mapper.Map<IEnumerable<WorksApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новую работу
        /// </summary>
        /// POST: /api/goods/
        [HttpPost]
        [ProducesResponseType(typeof(WorksApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(WorksRequestApiModel request, CancellationToken cancellationToken)
        {
            var requsetModel = mapper.Map<WorksCreateModel>(request);
            await validateService.Validate(requsetModel, CancellationToken.None);
            var result = await service.Create(requsetModel, cancellationToken);

            return Ok(mapper.Map<WorksCreateModel>(result));
        }

        /// <summary>
        /// Редактирует работу
        /// </summary>
        /// PUT: /api/goods/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<WorksApiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] WorksRequestApiModel request, CancellationToken cancellationToken)
        {
            var requsetModel = mapper.Map<WorksModel>(request);
            await validateService.Validate(requsetModel, CancellationToken.None);

            requsetModel.Id = id;

            var result = await service.Update(requsetModel, cancellationToken);

            return Ok(mapper.Map<WorksModel>(result));
        }
    }
}