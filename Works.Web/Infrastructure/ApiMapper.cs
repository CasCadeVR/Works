using AutoMapper;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Web.Models.Works;
using CasCadeVR.Works.Web.Models.Customers;
using CasCadeVR.Works.Web.Models.Executors;
using CasCadeVR.Works.Web.Models.ActWorks;
using CasCadeVR.Works.Web.Models.Acts;
using CasCadeVR.Works.Web.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Web.Infrastructure;

/// <summary>
/// Маппер для АПИ
/// </summary>
public class ApiMapper : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ApiMapper"/>
    /// </summary>
    public ApiMapper()
    {
        CreateMap<UnitOfMeasureModel, UnitOfMeasureApiModel>(MemberList.Destination);
        CreateMap<UnitOfMeasureCreateRequestApiModel, UnitOfMeasureCreateModel>(MemberList.Destination);

        CreateMap<WorksModel, WorkApiModel>(MemberList.Destination);
        CreateMap<WorkCreateRequestApiModel, WorksCreateModel>(MemberList.Destination);

        CreateMap<CustomerModel, CustomerApiModel>(MemberList.Destination);
        CreateMap<CustomerCreateRequestApiModel, CustomerCreateModel>(MemberList.Destination);

        CreateMap<ExecutorModel, ExecutorApiModel>(MemberList.Destination);
        CreateMap<ExecutorCreateRequestApiModel, ExecutorCreateModel>(MemberList.Destination);

        CreateMap<ActWorksModel, ActWorksApiModel>(MemberList.Destination);
        CreateMap<ActWorksCreateRequestApiModel, ActWorksCreateModel>(MemberList.Destination);

        CreateMap<ActModel, ActApiModel>(MemberList.Destination);
        CreateMap<ActCreateRequestApiModel, ActCreateModel>(MemberList.Destination);
    }
}