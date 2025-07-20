using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Works.Services.Contracts;
using Works.Web.Contracts.Models.Exceptions;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Contracts.IServices;
using Works.Web.Contracts.Models.Customers;

namespace Works.Web.Controllers
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
        /// ctor
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
        /// GET: /api/Customer/c2331ea8-a98d-4c3e-baea-d88f5665947
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
        /// GET: /api/Customer/
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
        /// POST: /api/Customer/
        [HttpPost]
        [ProducesResponseType(typeof(CustomerApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Create(CustomerRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestModel = mapper.Map<CustomerCreateModel>(request);
            await validateService.Validate(requestModel, CancellationToken.None);
            var result = await service.Create(requestModel, cancellationToken);

            return Ok(mapper.Map<CustomerApiModel>(result));
        }

        /// <summary>
        /// Редактирует заказчика по идентификатору
        /// </summary>
        /// PUT: /api/Customer/c2331ea8-a98d-4c3e-baea-d88f5665947
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CustomerApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] CustomerRequestApiModel request, CancellationToken cancellationToken)
        {
            var requestCreateModel = mapper.Map<CustomerCreateModel>(request);
            await validateService.Validate(requestCreateModel, CancellationToken.None);

            var requestModel = mapper.Map<CustomerModel>(request);
            requestModel.Id = id;

            var result = await service.Update(requestModel, cancellationToken);

            return Ok(mapper.Map<CustomerApiModel>(result));
        }

        /// <summary>
        /// Удаляет заказчика по идентификатору
        /// </summary>
        /// DELETE: /api/Customer/c2331ea8-a98d-4c3e-baea-d88f5665947
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