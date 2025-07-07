using AutoMapper;
using Works.Entities;
using Works.Services.Contracts.Models.Customers;
using Works.Services.Contracts.Models.Works;
using Works.Services.Contracts.Models.Executors;

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
    }
}
