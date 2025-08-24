using AutoMapper;
using CasCadeVR.Works.Services.Contracts.Models.Works;
using CasCadeVR.Works.Services.Contracts.Models.Customers;
using CasCadeVR.Works.Services.Contracts.Models.Executors;
using CasCadeVR.Works.Services.Contracts.Models.ActWorks;
using CasCadeVR.Works.Services.Contracts.Models.Acts;
using CasCadeVR.Works.Web.Contracts.Models.Works;
using CasCadeVR.Works.Web.Contracts.Models.Customers;
using CasCadeVR.Works.Web.Contracts.Models.Executors;
using CasCadeVR.Works.Web.Contracts.Models.ActWorks;
using CasCadeVR.Works.Web.Contracts.Models.Acts;

namespace CasCadeVR.Works.Web.Contracts.Infrastructure;

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
        CreateMap<WorksModel, WorkApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<WorksRequestApiModel, WorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<WorksRequestApiModel, WorksModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<CustomerModel, CustomerApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<CustomerRequestApiModel, CustomerCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<CustomerRequestApiModel, CustomerModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ExecutorModel, ExecutorApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<ExecutorRequestApiModel, ExecutorCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ExecutorRequestApiModel, ExecutorModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());

        CreateMap<ActWorksModel, ActWorksApiModel>(MemberList.Destination)
            .ConstructUsing(src => new ActWorksApiModel(
                src.Quantity,
                src.ActualPrice,
                src.TotalPrice,
                src.Work.Id,
                src.Work.Name,
                src.Work.Description,
                src.Work.Price,
                src.Work.UnitOfMeasure
            ));

        CreateMap<ActWorksRequestApiModel, ActWorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActWorksRequestApiModel, ActWorksModel>(MemberList.Destination)
            .ConstructUsing(src => new ActWorksModel
            {
                Work = new WorksModel { Id = src.WorkId }
            })
            .ForMember(x => x.TotalPrice, opt => opt.Ignore());

        CreateMap<ActModel, ActApiModel>(MemberList.Destination)
            .ForMember(dest => dest.ExecutorId, opt => opt.MapFrom(src => src.Executor.Id))
            .ForMember(dest => dest.ExecutorFIO, opt => opt.MapFrom(src => src.Executor.FIO))
            .ForMember(dest => dest.ExecutorFirm, opt => opt.MapFrom(src => src.Executor.Firm))
            .ForMember(dest => dest.ExecutorOccupation, opt => opt.MapFrom(src => src.Executor.Occupation))
            .ForMember(dest => dest.ExecutorOGRN, opt => opt.MapFrom(src => src.Executor.OGRN))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
            .ForMember(dest => dest.CustomerFIO, opt => opt.MapFrom(src => src.Customer.FIO))
            .ForMember(dest => dest.CustomerFirm, opt => opt.MapFrom(src => src.Customer.Firm))
            .ForMember(dest => dest.CustomerOccupation, opt => opt.MapFrom(src => src.Customer.Occupation))
            .ForMember(dest => dest.CustomerINN, opt => opt.MapFrom(src => src.Customer.INN))
            .ForMember(dest => dest.Works, opt => opt.MapFrom(src => src.Works));

        CreateMap<ActRequestApiModel, ActCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ActRequestApiModel, ActModel>(MemberList.Destination)
            .ConstructUsing(src => new ActModel {
                Executor = new ExecutorModel()
                {
                    Id = src.ExecutorId
                },
                Customer = new CustomerModel()
                {
                    Id = src.CustomerId
                },
                ActNumber = src.ActNumber,
                Date = src.Date,
                NDS = src.NDS,
            })
            .ForMember(dest => dest.Works, opt => opt.MapFrom(src => src.Works));
    }
}
