using AutoMapper;
using Works.Entities;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Contracts.Models.Works;
using Works.Services.Contracts.Models.Executors;
using Works.Services.Contracts.Models.ActWorks;
using Works.Services.Contracts.Models.Acts;

namespace Works.Services.Infrastructure;

/// <summary>
/// Маппер для сервисной части
/// </summary>
public class ServiceProfile : Profile
{
    /// <summary>
    /// ctor
    /// </summary>
    public ServiceProfile()
    {
        CreateMap<WorksModel, WorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Work, WorksModel>(MemberList.Destination).ReverseMap();

        CreateMap<CustomerModel, CustomerCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Customer, CustomerModel>(MemberList.Destination).ReverseMap();

        CreateMap<ExecutorModel, ExecutorCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Executor, ExecutorModel>(MemberList.Destination).ReverseMap();

        CreateMap<ActWorksModel, ActWorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActWork, ActWorksCreateModel>(MemberList.Destination);
        CreateMap<ActWorksCreateModel, ActWork>(MemberList.Destination)
            .ForMember(x => x.Work, opt => opt.Ignore())
            .ForMember(x => x.ActId, opt => opt.Ignore())
            .ForMember(x => x.Act, opt => opt.Ignore());

        CreateMap<ActWork, ActWorksModel>(MemberList.Destination)
            .ForMember(x => x.Work, opt => opt.MapFrom(y => y.Work)).ReverseMap();

        CreateMap<ActModel, ActCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Act, ActModel>(MemberList.Destination)
            .ForMember(x => x.Customer, opt => opt.MapFrom(y => y.Customer))
            .ForMember(x => x.Executor, opt => opt.MapFrom(y => y.Executor))
            .ForMember(x => x.Works, opt => opt.MapFrom(y => y.Works));
    }
}
