using AutoMapper;
using Works.Entities;
using Works.Services.Contracts.Models;

namespace Works.Services.Infrastructure;

/// <summary>
/// 
/// </summary>
public class ServiceProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public ServiceProfile()
    {
        CreateMap<WorksModel, WorksCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Work, WorksModel>(MemberList.Destination).ReverseMap();
    }
}
