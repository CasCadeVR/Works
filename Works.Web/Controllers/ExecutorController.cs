using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Web.Contracts.Models.Exceptions;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Web.Contracts.Models.Executors;

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
        /// GET: /api/Executor/c2331ea8-a98d-4c3e-baea-d88f5665947
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
        /// GET: /api/Executor/
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
        /// POST: /api/Executor/
        [HttpPost]
        [ProducesResponseType(typeof(ExecutorApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(ExecutorRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ExecutorCreateModel>(request);
            await validateService.Validate(requestModel, CancellationToken.None);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ExecutorApiModel>(result));
        }

        /// <summary>
        /// Редактирует исполнителя по идентификатору
        /// </summary>
        /// PUT: /api/Executor/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ExecutorApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] ExecutorRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ExecutorCreateModel>(request);
            await validateService.Validate(requestCreateModel, CancellationToken.None);

            var requestModel = mapper.Map<ExecutorModel>(request);
            requestModel.Id = id;

            var result = await service.Update(requestModel, cancellationToken);

            return Ok(mapper.Map<ExecutorApiModel>(result));
        }

        /// <summary>
        /// Удаляет исполнителя по идентификатору
        /// </summary>
        /// DELETE: /api/Executor/c2331ea8-a98d-4c3e-baea-d88f5665947
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