using AutoMapper;
using Works.Services.Contracts.Models.Works;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Contracts.Models.Executors;
using Works.Web.Models.Works;
using Works.Web.Models.Customers;
using Works.Web.Models.Executors;

namespace Works.Web.Infrastructure;

/// <summary>
/// Маппер для АПИ
/// </summary>
public class ApiMapper : Profile
{
    /// <summary>
    /// ctor
    /// </summary>
    public ApiMapper()
    {
        CreateMap<WorksRequestApiModel, WorksCreateModel>(MemberList.Destination);
        CreateMap<WorksRequestApiModel, WorksModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<WorksModel, WorkApiModel>(MemberList.Destination).ReverseMap();

        CreateMap<CustomerRequestApiModel, CustomerCreateModel>(MemberList.Destination);
        CreateMap<CustomerRequestApiModel, CustomerModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<CustomerModel, CustomerApiModel>(MemberList.Destination).ReverseMap();

        CreateMap<ExecutorRequestApiModel, ExecutorCreateModel>(MemberList.Destination);
        CreateMap<ExecutorRequestApiModel, ExecutorModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<ExecutorModel, ExecutorApiModel>(MemberList.Destination).ReverseMap();
    }
}
