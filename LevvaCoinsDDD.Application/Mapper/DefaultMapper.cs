using AutoMapper;
using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Dtos.User;
using LevvaCoinsDDD.Domain.Models;

namespace LevvaCoinsDDD.Application.Mapper;
public class DefaultMapper : Profile
{
    public DefaultMapper()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, LoginValuesDTO>().ReverseMap();
        CreateMap<User, UserUpdateDTO>().ReverseMap();
        CreateMap<Transaction, TransactionDTO>().ReverseMap();
        CreateMap<Transaction, TransactionNewDTO>().ReverseMap();
        CreateMap<Transaction, TransactionUpdateDTO>().ReverseMap();
        CreateMap<Transaction, TransactionResponseByUserDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
    }
}
