using AutoMapper;
using WebTechnologies.Application.Queries.UserQueries.GetSingle;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Mappers;
public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, SingleUserResponse>();
    }
}
