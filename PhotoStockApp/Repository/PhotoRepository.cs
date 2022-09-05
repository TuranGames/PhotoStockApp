using Microsoft.EntityFrameworkCore;
using PhotoStockApp.Data;
using PhotoStockApp.Interfaces;
using PhotoStockApp.Models;
using PhotoStockApp.Pagination;

namespace PhotoStockApp.Repository
{
    public class PhotoRepository : MainRepository<Photo>, IPhotoRepository
    {
        private readonly DataContext _context;

        public PhotoRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public int AverageRating(string ratingString)
        {
            
            IEnumerable<int> numbers = ratingString
            .Split('/')
            .Where(x => int.TryParse(x, out _))
            .Select(int.Parse)
            .ToList();
            int average = numbers.Sum() / numbers.Count();
            return average;
        }
        public bool RatePhoto(int photoId,int rating)
        {
            if(_context.Photos.Any(o => o.Id == photoId))
            {
                Photo photo = _context.Photos.FirstOrDefault(e => e.Id == photoId);
                photo.RatingString += "/" + rating;
                photo.Rating = AverageRating(photo.RatingString);
                return Save();
            }

            return false;
        }



    }
}
