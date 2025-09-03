using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Web.Models.Exceptions;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.IServices;
using CasCadeVR.Works.Web.Models.Customers;

namespace CasCadeVR.Works.Web.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с заказчиками
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices service;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CustomerController"/>
        /// </summary>
        public CustomerController(ICustomerServices service, IValidateService validateService, IMapper mapper)
        {
            this.service = service;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает заказчика по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CustomerApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await service.GetById(id, cancellationToken);

            return Ok(mapper.Map<CustomerApiModel>(result));
        }

        /// <summary>
        /// Получает список всех заказчиков
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerApiModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await service.GetAll(cancellationToken);

            return Ok(mapper.Map<IEnumerable<CustomerApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового заказчика
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(CustomerCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<CustomerCreateModel>(request);
            await validateService.Validate(requestModel, cancellationToken);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<CustomerApiModel>(result));
        }

        /// <summary>
        /// Редактирует заказчика по идентификатору
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CustomerApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] CustomerCreateRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<CustomerCreateModel>(request);
            await validateService.Validate(requestCreateModel, cancellationToken);

            var result = await service.Update(id, requestCreateModel, cancellationToken);

            return Ok(mapper.Map<CustomerApiModel>(result));
        }

        /// <summary>
        /// Удаляет заказчика по идентификатору
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