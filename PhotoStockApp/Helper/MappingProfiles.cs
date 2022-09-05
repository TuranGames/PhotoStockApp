using AutoMapper;
using PhotoStockApp.Dto;
using PhotoStockApp.Models;

namespace PhotoStockApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {


            CreateMap<AuthorDto, Author>();
            CreateMap<PhotoDto, Photo>();
            CreateMap<TextDto, Text>();


            CreateMap<Author, AuthorDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<Text, TextDto>();

        }
    }
}
