using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CasCadeVR.Works.Export.Contracts;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Web.Models.Acts;
using CasCadeVR.Works.Web.Models.Exceptions;

namespace CasCadeVR.Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с актами
    /// </summary>
    [ApiController]
    [Route("Api/[controller]")]
    public class ActController : ControllerBase
    {
        private readonly IActServices service;
        private readonly IExporter exporter;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ActController"/>
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
        [HttpGet("{id:guid}/export")]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ExportById([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var exportedData = await service.Export(id, cancellationToken);

            return File(exportedData.ExportedMemoryStream.ToArray(), exportedData.FileType, exportedData.FileName);
        }

        /// <summary>
        /// Получает акт по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ActApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);
            return Ok(mapper.Map<ActApiModel>(result));
        }

        /// <summary>
        /// Получает список всех актов
        /// </summary>
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
        [HttpPost]
        [ProducesResponseType(typeof(ActApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]

        public async Task<ActionResult> Create(ActCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ActCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await service.Create(requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ActApiModel>(result));
        }

        /// <summary>
        /// Редактирует акт по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ActApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]ActCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<ActCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);
            var result = await service.Update(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<ActApiModel>(result));
        }

        /// <summary>
        /// Удаляет акт по идентификатору
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