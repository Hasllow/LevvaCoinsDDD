using AutoMapper;
using LevvaCoinsDDD.Application.Dtos;
using LevvaCoinsDDD.Domain.Models;

namespace LevvaCoinsDDD.Application.Mapper;
public class DefaultMapper : Profile
{
    public DefaultMapper()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserUpdateDTO>().ReverseMap();
        CreateMap<Transaction, TransactionDTO>().ReverseMap();
        CreateMap<Transaction, TransactionUpdateDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
    }
}
