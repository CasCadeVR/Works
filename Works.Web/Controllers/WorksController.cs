using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Web.Contracts.Models.Exceptions;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Web.Contracts.Models.Works;

namespace CasCadeVR.Works.Web.Controllers
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
        /// Инициализирует новый экземпляр <see cref="WorksController"/>
        /// </summary>
        public WorksController(IWorksServices service, IValidateService validateService, IMapper mapper)
        {
            this.service = service;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает работу по идентификатору
        /// </summary>
        /// GET: /api/Works/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(WorkApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);

            return Ok(mapper.Map<WorkApiModel>(result));
        }

        /// <summary>
        /// Получает список всех работ
        /// </summary>
        /// GET: /api/Works/
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WorkApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await service.GetAll(cancellationToken);

            return Ok(mapper.Map<IEnumerable<WorkApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новую работу
        /// </summary>
        /// POST: /api/Works/
        [HttpPost]
        [ProducesResponseType(typeof(WorkApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(WorksRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<WorksCreateModel>(request);
            await validateService.Validate(requestModel, CancellationToken.None);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<WorkApiModel>(result));
        }

        /// <summary>
        /// Редактирует работу по идентификатору
        /// </summary>
        /// PUT: /api/Works/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(WorkApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]WorksRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<WorksCreateModel>(request);
            await validateService.Validate(requestCreateModel, CancellationToken.None);

            var requestModel = mapper.Map<WorksModel>(request);
            requestModel.Id = id;

            var result = await service.Update(requestModel, cancellationToken);

            return Ok(mapper.Map<WorkApiModel>(result));
        }

        /// <summary>
        /// Удаляет работу по идентификатору
        /// </summary>
        /// DELETE: /api/Works/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await service.Delete(id, cancellationToken);
            return Ok();
        }
    }
}