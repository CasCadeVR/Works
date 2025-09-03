using AutoMapper;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;

namespace CasCadeVR.Works.Services.Infrastructure;

/// <summary>
/// Маппер для сервисной части
/// </summary>
public class ServiceProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ServiceProfile"/>
    /// </summary>
    public ServiceProfile()
    {
        CreateMap<UnitOfMeasureModel, UnitOfMeasureCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<UnitOfMeasureCreateModel, UnitOfMeasure>(MemberList.Source)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<UnitOfMeasure, UnitOfMeasureModel>(MemberList.Destination).ReverseMap();

        CreateMap<WorksModel, WorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<WorksCreateModel, Work>(MemberList.Source)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Work, WorksModel>(MemberList.Destination);

        CreateMap<CustomerModel, CustomerCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<CustomerCreateModel, Customer>(MemberList.Source)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Customer, CustomerModel>(MemberList.Destination).ReverseMap();

        CreateMap<ExecutorModel, ExecutorCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ExecutorCreateModel, Executor>(MemberList.Source)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<Executor, ExecutorModel>(MemberList.Destination).ReverseMap();

        CreateMap<ActWorksModel, ActWorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActWork, ActWorksCreateModel>(MemberList.Destination);
        CreateMap<ActWorksCreateModel, ActWork>(MemberList.Source)
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Work, opt => opt.Ignore())
            .ForMember(x => x.ActId, opt => opt.Ignore())
            .ForMember(x => x.Act, opt => opt.Ignore());

        CreateMap<ActWork, ActWorksModel>(MemberList.Destination).ReverseMap();

        CreateMap<ActModel, ActCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Act, ActModel>(MemberList.Destination);

    }
}
