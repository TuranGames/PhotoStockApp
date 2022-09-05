using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStockApp.Dto;
using PhotoStockApp.Interfaces;
using PhotoStockApp.Models;
using PhotoStockApp.Pagination;
using PhotoStockApp.Repository;
using System.Diagnostics.Metrics;
using System.Net;

namespace PhotoStockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _PhotoRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public PhotoController(IPhotoRepository PhotoRepository,IAuthorRepository authorRepository,
            IMapper mapper)
        {
            _PhotoRepository = PhotoRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PhotoDto>))]
        public IActionResult GetPhotos()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorNickName, act => act.MapFrom(src => src.Author.NickName));

            });
            var photos = new Mapper(config).Map<List<PhotoDto>>(_PhotoRepository.GetAll());



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(photos);

        }


        [HttpGet("GetPhoto/{PhotoId}")]
        [ProducesResponseType(200, Type = typeof(Photo))]
        [ProducesResponseType(400)]
        public IActionResult GetPhoto(int PhotoId)
        {
            if (!_PhotoRepository.Exists(PhotoId))
                return NotFound();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorNickName, act => act.MapFrom(src => src.Author.NickName));

            });
            var author = new Mapper(config).Map<PhotoDto>(_PhotoRepository.Get(PhotoId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(author);
        }

        [HttpPatch("RatePhoto/{PhotoId}")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult RatePhoto(int PhotoId, [FromBody] int rating)
        {
            if (rating > 5 && rating < 0)
            {
                ModelState.AddModelError("", "Rating can not be 0 or higher then 5");
                return StatusCode(500, ModelState);
            }
            if (!_PhotoRepository.Exists(PhotoId))
                return NotFound();

            if(!_PhotoRepository.RatePhoto(PhotoId, rating))
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPut("UpdatePhoto/{PhotoId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePhoto(int PhotoId, [FromBody] PhotoDto updatedPhoto)
        {
            if (updatedPhoto == null)
                return BadRequest(ModelState);

            if (!_PhotoRepository.Exists(PhotoId))
                return NotFound();

            Photo updatingPhoto = _PhotoRepository.Get(PhotoId);
            
            if (!ModelState.IsValid)
                return BadRequest();

            
            var photoMap = _mapper.Map(updatedPhoto, updatingPhoto);
            bool nickExists = _PhotoRepository.GetAll().FirstOrDefault(e => e.Author.NickName == updatedPhoto.AuthorNickName) != null;
            if (!nickExists)
            {
                photoMap.Author = new Author()
                {
                    NickName = updatedPhoto.AuthorNickName,
                    FirstName = updatedPhoto.AuthorName,
                    LastName = "",
                };
            }
            if (!_PhotoRepository.Update(photoMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("Pagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PaginatedPhoto>))]
        [ProducesResponseType(400)]
        public IActionResult  GetPagination([FromQuery] QueryParams qp)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.AuthorName, act => act.MapFrom(src => src.Author.FirstName))
                .ForMember(dest => dest.AuthorNickName, act => act.MapFrom(src => src.Author.NickName));

            });
            IEnumerable<PhotoDto> photos = new Mapper(config).Map<List<PhotoDto>>(_PhotoRepository.GetOffset());
            PaginatedPhoto res = PaginatedPhoto.ToPaginatedPhoto(photos, qp.Page, qp.PhotosPerPage);
            return Ok(res);
        }
    }
}
