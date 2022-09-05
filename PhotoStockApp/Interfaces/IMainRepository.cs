using PhotoStockApp.Models;
using PhotoStockApp.Pagination;

namespace PhotoStockApp.Interfaces
{
    public interface IMainRepository<T> 
    {
        ICollection<T> GetAll();
        T Get(int textid);
        bool Exists(int textId);
        bool Create(T text);
        bool Update(T text);
        bool Delete(T text);
        ICollection<T> GetOffset();
        bool Save();
    }
}
