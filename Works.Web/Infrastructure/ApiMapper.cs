using AutoMapper;
using Works.Services.Contracts.Models;
using Works.Web.Models;

namespace Works.Web.Infrastructure;

/// <summary>
/// 
/// </summary>
public class ApiMapper : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public ApiMapper()
    {
        CreateMap<WorksRequestApiModel, WorksCreateModel>(MemberList.Destination);
        CreateMap<WorksRequestApiModel, WorksModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<WorksModel, WorksApiModel>(MemberList.Destination);
    }
}
