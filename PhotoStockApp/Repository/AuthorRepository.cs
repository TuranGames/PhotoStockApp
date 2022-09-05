using Microsoft.EntityFrameworkCore;
using PhotoStockApp.Data;
using PhotoStockApp.Interfaces;
using PhotoStockApp.Models;

namespace PhotoStockApp.Repository
{
    public class AuthorRepository : MainRepository<Author>, IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepository(DataContext context):base(context)
        {
            _context = context;
        }
        public ICollection<Author> GetAll()
        {
            string sql = "SELECT * FROM Authors";
            List<Author> res = _context.Authors.FromSqlRaw(sql).ToList();

            return res;
        }

    }
}
