using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Data.Entities;
using UrlShortener.Data.Models.DTO;

namespace UrlShortener.Services
{
    public class CategoryServices
    {
        private readonly UrlShortenerContext _context;
        public CategoryServices(UrlShortenerContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category? GetByName(string categoryName)
        {
            return _context.Categories.SingleOrDefault(c => c.NameCategory.ToLower() == categoryName.ToLower());
        }
        public Category? GetById(int id) 
        {
            return _context.Categories.SingleOrDefault(cat => cat.Id == id);
        }



        public int Add(CategoryDto category)
        {
            Category newCategory = new Category()
            {
                NameCategory = category.NameCategory,
 
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return newCategory.Id;
        }

        public bool Delete(int id)
        {
            Category? categoryToDelete = GetById(id);
            if (categoryToDelete == null) return false;
            _context.Categories.Remove(categoryToDelete);
            _context.SaveChanges();
            return true;
        }


    }
}
