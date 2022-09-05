using Microsoft.EntityFrameworkCore;
using PhotoStockApp.Data;
using PhotoStockApp.Interfaces;
using PhotoStockApp.Models;

namespace PhotoStockApp.Repository
{
    public class TextRepository : MainRepository<Text>, ITextRepository
    {
        private readonly DataContext _context;

        public TextRepository(DataContext context) : base(context)
        {
            _context = context;
        }
  
    }
}
