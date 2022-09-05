using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoStockApp.Models
{
   public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RatingString { get; set; }
        public int Rating { get; set; }
        public DateTime DateOfCreation { get; set; }
        public long Cost { get; set; }
        public int NumberOfSales { get; set; }
        public Author Author { get; set; }
       

    }
}

