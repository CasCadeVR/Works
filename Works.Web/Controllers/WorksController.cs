using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Web.Models.Exceptions;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Web.Models.Works;

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
        [HttpPost]
        [ProducesResponseType(typeof(WorkApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create(WorkCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<WorksCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await service.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<WorkApiModel>(result));
        }

        /// <summary>
        /// Редактирует работу по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(WorkApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]WorkCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<WorksCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await service.Update(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<WorkApiModel>(result));
        }

        /// <summary>
        /// Удаляет работу по идентификатору
        /// </summary>
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