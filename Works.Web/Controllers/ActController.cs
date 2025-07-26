using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Works.Export.Contracts;
using Works.Services.Contracts;
using Works.Services.Contracts.IServices;
using Works.Services.Contracts.Models.Acts;
using Works.Web.Contracts.Models.Acts;
using Works.Web.Contracts.Models.Exceptions;

namespace Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с актами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ActController : ControllerBase
    {
        private readonly IActServices service;
        private readonly IExporter exporter;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public ActController(IActServices service, IExporter exporter, IValidateService validateService, IMapper mapper)
        {
            this.service = service;
            this.exporter = exporter;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Экспортирует акт по идентификатору
        /// </summary>
        /// GET: /api/Act/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpGet("{id:guid}/export")]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ExportById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);
            var excelBytes = exporter.Export(mapper.Map<ActApiModel>(result));
            var fileName = $"Act_{result.ActNumber}_{result.Date:yyyy-MM-dd}.xlsx";

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// Получает акт по идентификатору
        /// </summary>
        /// GET: /api/Act/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ActApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);

            return Ok(mapper.Map<ActApiModel>(result));
        }

        /// <summary>
        /// Получает список всех актов
        /// </summary>
        /// GET: /api/Act/
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await service.GetAll(cancellationToken);
            Console.WriteLine(result);

            return Ok(mapper.Map<IEnumerable<ActApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новый акт
        /// </summary>
        /// POST: /api/Act/
        [HttpPost]
        [ProducesResponseType(typeof(ActApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]

        public async Task<ActionResult> Create(ActRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<ActCreateModel>(request);
            await validateService.Validate(requestModel, CancellationToken.None);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<ActApiModel>(result));
        }

        /// <summary>
        /// Редактирует акт по идентификатору
        /// </summary>
        /// PUT: /api/Act/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ActApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]ActRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ActCreateModel>(request);
            await validateService.Validate(requestCreateModel, CancellationToken.None);

            var requestModel = mapper.Map<ActModel>(request);
            requestModel.Id = id;

            var result = await service.Update(requestModel, cancellationToken);

            return Ok(mapper.Map<ActApiModel>(result));
        }

        /// <summary>
        /// Удаляет акт по идентификатору
        /// </summary>
        /// DELETE: /api/Act/c2331ea8-a98d-4c3e-baea-d88f5665947
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