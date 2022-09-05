using Microsoft.Extensions.Hosting;
using PhotoStockApp.Models;
using PhotoStockApp.Dto;

namespace PhotoStockApp.Pagination
{
    public class PaginatedText
    {
        public PaginatedText(
            IEnumerable<TextDto> items, int count, int pageNumber, int textsPerPage)
        {
            PageInfo = new PageInfo
            {
                CurrentPage = pageNumber,
                ContnentsPerPage = textsPerPage,
                TotalPages = (int)Math.Ceiling(count / (double)textsPerPage),
                TotalContents = count
            };
            Texts = items;
        }

        public PageInfo PageInfo { get; set; }

        public IEnumerable<TextDto> Texts { get; set; }

        public static PaginatedText ToPaginatedText(
            IEnumerable<TextDto> texts, int pageNumber, int textsPerPage)
        {
            var count = texts.Count();
            var chunk = texts.Skip((pageNumber - 1) * textsPerPage).Take(textsPerPage);
            return new PaginatedText(chunk, count, pageNumber, textsPerPage);
        }
    }


}
