using Application.DTOs.Author;
using Application.DTOs.Book;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorDto, AuthorCreateOrUpdateDto>().ReverseMap();

        }
    }
}
