
namespace PhotoStockApp.Dto
{
   public abstract class ProductDto
    {
        
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public long Cost { get; set; }
        public string AuthorName { get; set; }
        public string AuthorNickName { get; set; }
        public int NumberOfSales { get; set; }
        public int Rating { get; set; }


      


    }
}

