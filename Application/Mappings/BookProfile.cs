using Application.DTOs.Book;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile() {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookDto, CreateOrUpdateDto>().ReverseMap();
        }
    }
}
