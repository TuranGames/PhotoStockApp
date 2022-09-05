using PhotoStockApp.Models;
using PhotoStockApp.Pagination;

namespace PhotoStockApp.Interfaces
{
    public interface IPhotoRepository:IMainRepository<Photo>
    {

        bool RatePhoto(int photoId, int rating);
        public int AverageRating(string ratingString);
    }
}
