using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStockApp.Data;
using PhotoStockApp.Dto;
using PhotoStockApp.Interfaces;
using PhotoStockApp.Models;
using PhotoStockApp.Repository;
using System.Net;

namespace PhotoStockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository,
            IMapper mapper)
        {
            _authorRepository = authorRepository;

            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthors()
        {
            // Returns FirsName+LastName as Name
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.FirstName + " " + src.LastName));

            });
            var authors = new Mapper(config).Map<List<AuthorDto>>(_authorRepository.GetAll());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(authors);

        }

     

     
    }
}
