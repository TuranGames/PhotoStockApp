using Microsoft.Extensions.Hosting;
using PhotoStockApp.Models;
using PhotoStockApp.Dto;

namespace PhotoStockApp.Pagination
{
    public class PaginatedPhoto
    {
        public PaginatedPhoto(IEnumerable<PhotoDto> items, int count, int pageNumber, int photosPerPage)
        {
            PageInfo = new PageInfo
            {
                CurrentPage = pageNumber,
                ContnentsPerPage = photosPerPage,
                TotalPages = (int)Math.Ceiling(count / (double)photosPerPage),
                TotalContents = count
            };
            Photos = items;
        }

        public PageInfo PageInfo { get; set; }

        public IEnumerable<PhotoDto> Photos { get; set; }

        public static PaginatedPhoto ToPaginatedPhoto(IEnumerable<PhotoDto> photos, int pageNumber, int photosPerPage)
        {
            var count = photos.Count();
            var chunk = photos.Skip((pageNumber - 1) * photosPerPage).Take(photosPerPage);
            return new PaginatedPhoto(chunk, count, pageNumber, photosPerPage);
        }
    }


}
