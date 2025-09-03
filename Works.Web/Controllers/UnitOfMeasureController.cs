using AutoMapper;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;
using CasCadeVR.Works.Web.Models.Exceptions;
using CasCadeVR.Works.Web.Models.UnitOfMeasure;
using Microsoft.AspNetCore.Mvc;

namespace CasCadeVR.Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с единицами измерения
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly IUnitOfMeasureServices service;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="UnitOfMeasureController"/>
        /// </summary>
        public UnitOfMeasureController(IUnitOfMeasureServices service, IValidateService validateService, IMapper mapper)
        {
            this.service = service;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает единицу измерения по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UnitOfMeasureApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);

            return Ok(mapper.Map<UnitOfMeasureApiModel>(result));
        }

        /// <summary>
        /// Получает список всех единиц измерения
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UnitOfMeasureApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await service.GetAll(cancellationToken);

            return Ok(mapper.Map<IEnumerable<UnitOfMeasureApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новую единицу измерения
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UnitOfMeasureApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(UnitOfMeasureCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<UnitOfMeasureCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<UnitOfMeasureApiModel>(result));
        }

        /// <summary>
        /// Редактирует единицу измерения по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UnitOfMeasureApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] UnitOfMeasureCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<UnitOfMeasureCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);

            var result = await service.Update(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<UnitOfMeasureApiModel>(result));
        }

        /// <summary>
        /// Удаляет единицу измерения по идентификатору
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