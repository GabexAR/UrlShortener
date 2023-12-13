using UrlShortener.Helpers;
using UrlShortener.Data.Entities;
using UrlShortener.Data;
using UrlShortener.Data.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Services
{
    public class UrlService
    {
        private readonly UrlShortenerContext _context;
        public UrlService(UrlShortenerContext context)
        {
            _context = context;
        }
        public List<Url> GetAll()
        {
            return _context.Urls.ToList();
        }
        public List<Url> GetByUserId(int id)
        {
            return _context.Urls.Where(url => url.UserId == id).ToList();
        }
        public Url? GetById(int id)
        {
            return _context.Urls.SingleOrDefault(url => url.Id == id);
        }
        public Url? GetByCode(string shortUrl)
        {
            return _context.Urls.SingleOrDefault(url => url.ShortUrl == shortUrl);
        }

        public List<UrlDto> GetByCategory(int id) 
        {
            return _context.Urls.Include(url => url.Category).Where(c => c.Category.Id == id).Select(url => new UrlDto()
            {
                Id = url.Id,
                ShortUrl = url.ShortUrl,
                LargeUrl    = url.LargeUrl,
                CategoryId  = url.CategoryId
            }).ToList();
        }
        public int Add(UrlCreation url, int userId, string categoryName)
        {
            var user = _context.Users.Find(userId);
            var categoria = _context.Categories.FirstOrDefault(c => c.NameCategory.ToLower() == categoryName.ToLower());
            Console.WriteLine(categoria) ;

            if (user.CantConversiones > 0) {

                if (categoria != null)
                {


                    Url newUrl = new Url()
                    {
                        LargeUrl = url.Url,
                        ShortUrl = Shortener.GetShortUrl(),
                        UserId = userId,
                        CategoryId = categoria.Id
                    };
                _context.Urls.Add(newUrl);
                user.CantConversiones--;
                Console.WriteLine(user.CantConversiones);
                _context.SaveChanges();
                return newUrl.Id;
            };

        }
            return user.CantConversiones;
        }
        public bool Update(int id)
        {
            Url? urlToUpd = GetById(id);
            if (urlToUpd != null)
            {
                urlToUpd.ShortUrl = Shortener.GetShortUrl();
                _context.Urls.Update(urlToUpd);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateClicks(int id)
        {
            Url? urlToUpd = GetById(id);
            if (urlToUpd == null) return false;
            urlToUpd.Count++;
            _context.Urls.Update(urlToUpd);
            _context.SaveChanges();
            return true;

        }
        public bool Delete(int id)
        {
            Url? urlToDel = GetById(id);
            if (urlToDel == null) return false;
            _context.Urls.Remove(urlToDel);
            _context.SaveChanges();
            return true;
        }
        public void DeleteByUser(int id)
        {
            List<Url> urls = _context.Urls.Where(u => u.UserId == id).ToList();
            foreach (Url url in urls)
                _context.Remove(url);
            _context.SaveChanges();
        }
    }

}
