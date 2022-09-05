using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoStockApp.Data;
using PhotoStockApp.Models;
using PhotoStockApp.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Reflection;

namespace PhotoStockApp.Repository
{
    public class MainRepository<T> : IMainRepository<T> where T : class
    {
        private readonly DataContext _context;
        public MainRepository(DataContext context)
        {
           
            _context = context;

        }

        public bool Create(T table)
        {
            _context.Add(table);
            return Save();
        }
        public bool Delete(T table)
        {
            _context.Remove(table);
            return Save();
        }
        public T Get(int id)
        {
            string sql = "SELECT * FROM " + typeof(T).Name + "s WHERE Id = " + id;
            List<T> g = _context.Set<T>().FromSqlRaw(sql).Include("Author").ToList<T>();

            return g[0];
        }
        public ICollection<T> GetOffset()
        {
            string sql = "SELECT * FROM " + typeof(T).Name + "s ORDER BY Id DESC";

            return _context.Set<T>().FromSqlRaw(sql).Include("Author").ToList<T>();
        }
        public ICollection<T> GetAll()
        {
            string sql = "SELECT * FROM " + typeof(T).Name + "s";

            return _context.Set<T>().FromSqlRaw(sql).Include("Author").ToList<T>();
        }
        public bool Exists(int Id)
        {
            return _context.Photos.Any(o => o.Id == Id);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Update(T table)
        {
            _context.Update(table);
            return Save();
        }
    }
}
