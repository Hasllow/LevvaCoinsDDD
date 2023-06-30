using AutoMapper;
using LevvaCoinsDDD.Application.Commands.Requests.User;
using LevvaCoinsDDD.Application.Commands.Response.User;
using LevvaCoinsDDD.Application.Dtos.Category;
using LevvaCoinsDDD.Application.Dtos.Transaction;
using LevvaCoinsDDD.Application.Dtos.User;
using LevvaCoinsDDD.Application.Queries.Responses.User;
using LevvaCoinsDDD.Domain.Models;

namespace LevvaCoinsDDD.Application.Mapper;
public class DefaultMapper : Profile
{
    public DefaultMapper()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<User, UserUpdateDTO>().ReverseMap();
        CreateMap<User, LoginValuesDTO>().ReverseMap();

        CreateMap<User, LoginUserResponse>().ReverseMap();
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, CreateUserResponse>().ReverseMap();
        CreateMap<User, GetUserByIdResponse>().ReverseMap();
        CreateMap<User, GetAllUsersResponse>().ReverseMap();



        CreateMap<Transaction, TransactionDTO>().ReverseMap();
        CreateMap<Transaction, TransactionNewDTO>().ReverseMap();
        CreateMap<Transaction, TransactionUpdateDTO>().ReverseMap();
        CreateMap<Transaction, TransactionResponseByUserDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryNewAndUpdateDTO>().ReverseMap();
        CreateMap<CategoryDTO, CategoryNewAndUpdateDTO>().ReverseMap();
    }
}
