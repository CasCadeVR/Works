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
        CreateMap<UnitOfMeasureModel, UnitOfMeasureApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<UnitOfMeasureCreateRequestApiModel, UnitOfMeasureCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<UnitOfMeasureCreateRequestApiModel, UnitOfMeasureModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<WorksModel, WorkApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<WorkCreateRequestApiModel, WorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<WorkCreateRequestApiModel, WorksModel>(MemberList.Destination)
            .ForMember(x => x.UnitOfMeasure, opt => opt.MapFrom(y => new UnitOfMeasureModel { Id = y.UnitOfMeasureId }))
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<CustomerModel, CustomerApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<CustomerCreateRequestApiModel, CustomerCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<CustomerCreateRequestApiModel, CustomerModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ExecutorModel, ExecutorApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<ExecutorCreateRequestApiModel, ExecutorCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ExecutorCreateRequestApiModel, ExecutorModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ActWorksModel, ActWorksApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActWorksCreateRequestApiModel, ActWorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActWorksCreateRequestApiModel, ActWorksModel>(MemberList.Destination)
            .ForMember(x => x.Work, opt => opt.MapFrom(y => new WorksModel { Id = y.WorkId }));

        CreateMap<ActModel, ActApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActCreateRequestApiModel, ActCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActCreateRequestApiModel, ActModel>(MemberList.Destination)
            .ForMember(x => x.Executor, opt => opt.MapFrom(y => new ExecutorModel { Id = y.ExecutorId }))
            .ForMember(x => x.Customer, opt => opt.MapFrom(y => new CustomerModel { Id = y.CustomerId }));
    }
}
