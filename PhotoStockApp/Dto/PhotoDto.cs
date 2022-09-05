using PhotoStockApp.Models;

namespace PhotoStockApp.Dto
{
    public class PhotoDto : ProductDto

    {
        public float OriginalSize { get; set; }
        public string Link { get; set; }


    }
}
