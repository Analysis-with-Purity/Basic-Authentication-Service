using API.Entities;
using AuthMicroservice.Dtos;
using AutoMapper;

namespace Infrastructure.Data;

public class AuthMappings : Profile
{
    public AuthMappings()
    {
        CreateMap<User, RegisterRequest>().ReverseMap();
    }
}