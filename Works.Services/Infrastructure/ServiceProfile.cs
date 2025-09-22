using AutoMapper;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Services.Contracts.Models.UnitOfMeasure;
using CasCadeVR.Works.Repository.Contracts.Models;

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
        CreateMap<UnitOfMeasureCreateModel, UnitOfMeasure>(MemberList.Source);
        CreateMap<UnitOfMeasure, UnitOfMeasureModel>(MemberList.Destination);

        CreateMap<WorksCreateModel, Work>(MemberList.Source);
        CreateMap<Work, WorksModel>(MemberList.Destination);

        CreateMap<CustomerCreateModel, Customer>(MemberList.Source);
        CreateMap<Customer, CustomerModel>(MemberList.Destination);

        CreateMap<ExecutorCreateModel, Executor>(MemberList.Source);
        CreateMap<Executor, ExecutorModel>(MemberList.Destination);

        CreateMap<ActWorksCreateModel, ActWork>(MemberList.Source);
        CreateMap<ActWorksModel, ActWorksCreateModel>(MemberList.Destination);
        CreateMap<ActWork, ActWorksModel>(MemberList.Destination);

        CreateMap<ActModel, ActCreateModel>(MemberList.Destination);
        CreateMap<Act, ActModel>(MemberList.Destination);

        CreateMap<ActDbModel, Act>(MemberList.Source);
        CreateMap<ActDbModel, ActModel>(MemberList.Source);
        CreateMap<ActWorkDbModel, ActWork>(MemberList.Source);
        CreateMap<ActWorkDbModel, ActWorksModel>(MemberList.Destination);

        CreateMap<WorkDbModel, Work>(MemberList.Source);
        CreateMap<WorkDbModel, WorksModel>(MemberList.Source);
    }
}