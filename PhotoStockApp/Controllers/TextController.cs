using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStockApp.Dto;
using PhotoStockApp.Interfaces;
using PhotoStockApp.Models;
using PhotoStockApp.Pagination;
using PhotoStockApp.Repository;
using System.Net;

namespace PhotoStockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextRepository _textRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public TextController(ITextRepository textRepository,IAuthorRepository authorRepository,
            IMapper mapper)
        {
            _textRepository = textRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet("GetTexts")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TextDto>))]
        public IActionResult GetTexts()
        {

            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Text, TextDto>()
                .ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorNickName, act => act.MapFrom(src => src.Author.NickName));

            });
            var texts = new Mapper(config).Map<List<TextDto>>(_textRepository.GetAll());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(texts);

        }

        [HttpPost("CreateText")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateText([FromBody] TextDto textCreate)
        {
            if (textCreate == null)
                return BadRequest(ModelState);

            
            Author? author = _authorRepository.GetAll()
                .Where(c => c.NickName.Trim().ToUpper() == textCreate.AuthorNickName.TrimEnd().ToUpper())
                .FirstOrDefault();
            bool textExists = _textRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == textCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault() != null;

            if (textExists)
            {
                ModelState.AddModelError("", "Text already exists");
                return StatusCode(422, ModelState);
            }

            if (author == null)
            {
                ModelState.AddModelError("", "Author doesn't exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var textMap = _mapper.Map<Text>(textCreate);

            textMap.Author = author;

            if (!_textRepository.Create(textMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpGet("Pagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PaginatedPhoto>))]
        [ProducesResponseType(400)]
        public IActionResult GetPagination([FromQuery] QueryParams qp)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Text, TextDto>()
                .ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorNickName, act => act.MapFrom(src => src.Author.NickName));

            });
            IEnumerable<TextDto> text = new Mapper(config).Map<List<TextDto>>(_textRepository.GetOffset());
            PaginatedText res = PaginatedText.ToPaginatedText(text, qp.Page, qp.PhotosPerPage);
            return Ok(res);
        }

    }
}
