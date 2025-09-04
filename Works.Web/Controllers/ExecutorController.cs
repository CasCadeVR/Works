using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Web.Models.Exceptions;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Web.Models.Executors;

namespace CasCadeVR.Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с исполнителями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutorController : ControllerBase
    {
        private readonly IExecutorServices service;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExecutorController"/>
        /// </summary>
        public ExecutorController(IExecutorServices service, IValidateService validateService, IMapper mapper)
        {
            this.service = service;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает исполнителя по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ExecutorApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);
            return Ok(mapper.Map<ExecutorApiModel>(result));
        }

        /// <summary>
        /// Получает список всех исполнителей
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExecutorApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await service.GetAll(cancellationToken);
            return Ok(mapper.Map<IEnumerable<ExecutorApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового исполнителя
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ExecutorApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create(ExecutorCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ExecutorCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await service.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ExecutorApiModel>(result));
        }

        /// <summary>
        /// Редактирует исполнителя по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ExecutorApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] ExecutorCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ExecutorCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await service.Update(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ExecutorApiModel>(result));
        }

        /// <summary>
        /// Удаляет исполнителя по идентификатору
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