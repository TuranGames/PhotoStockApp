using PhotoStockApp.Data;
using PhotoStockApp.Models;

namespace PhotoStockApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        private readonly string[] arr;
        public Seed(DataContext context)
        {
            this.dataContext = context;
            arr = File.ReadAllText(Environment.CurrentDirectory+"/Names.txt").Split(",");
        }
        public void SeedDataContext()
        {
            if (!dataContext.Photos.Any())
            {
                List<Photo> photos = new List<Photo>();
                for (int i = 0; i < 50; i++)
                {
                    photos.Add(new Photo()
                    {
                        OriginalSize = new Random().Next(3000, 9999),
                        Link = "google.com",
                        Name = "NewPhoto.png",
                        Cost = new Random().Next(300, 900),
                        DateOfCreation = DateTime.Now,
                        RatingString = "",
                        Author = new Author
                        {
                            FirstName = RandomName(arr),
                            NickName = RandomName(arr),
                            LastName = RandomName(arr)
                        }
                    });
                }

                dataContext.Photos.AddRange(photos);
                dataContext.SaveChanges();

            }
            if (!dataContext.Texts.Any())
            {
                List<Text> texts = new List<Text>();
                for (int i = 0; i < 50; i++)
                {
                    texts.Add(new Text()
                    {
                        textContetnt = "Some cool text is here",
                        Name = "NewPhoto.png",
                        Cost = new Random().Next(300, 900),
                        DateOfCreation = DateTime.Now,
                        RatingString = "",
                        Author = new Author
                        {
                            FirstName = RandomName(arr),
                            NickName = RandomName(arr),
                            LastName = RandomName(arr)
                        }
                    });
                }




                dataContext.Texts.AddRange(texts);
                dataContext.SaveChanges();

            }
        }
        private string RandomName(string[] arr)
        {
            string res = arr[new Random().Next(0, arr.Length)];
            return res;
        }
    }
}