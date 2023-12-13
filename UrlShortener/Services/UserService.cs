    using UrlShortener.Data;
    using UrlShortener.Data.Entities;
    using UrlShortener.Data.Models.DTO;
    using UrlShortener.Helpers;


    namespace UrlShortener.Services
    {
        public class UserService
        {
            private readonly UrlShortenerContext _context;
            private readonly UrlService _urlService;
            public UserService(UrlShortenerContext urlShortenerContext, UrlService urlService)
            {
                _context = urlShortenerContext;
                _urlService = urlService;
            }
            public IEnumerable<UserDto> GetAll()
            {
                return _context.Users.Select(u => new UserDto()
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    Role = u.Role
                }).ToList();
            }
            public UserDto? GetById(int id)
            {
                User? user = _context.Users.SingleOrDefault(user => user.Id == id);
                return user is null ? null : new UserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            public UserDto? GetByEmail(string email)
            {
                User? user = _context.Users.SingleOrDefault(user => user.Email == email.ToLower());
                return user is null ? null : new UserDto()
                {
                    Id = user.Id,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email,
                    Role = user.Role
                };
            }
            public int GetCantConver(int userId) { 
           
            var user = _context.Users.Find(userId);
            Console.WriteLine(user.CantConversiones);

             if (user!= null)
                {
                return user.CantConversiones;
                }
             return -1;
          
            }
            public int ResetCantCover(int userId) 
            {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.CantConversiones = 10;
                _context.SaveChanges();

                return 1;
            }
            return -1;
        }
        public bool Authenticate(string email, string password)
            {
                UserDto? user = GetByEmail(email);
                if (user == null) return false;
                return user.PasswordHash == Hashing.GetPasswordHash(password);
            }
            public bool Exists(string email)
            {
                return GetByEmail(email.ToLower()) is not null;
            }
            public void Add(UserCreationDto dto)
            {
                User newUser = new User()
                {
                    Username = dto.Username,
                    Email = dto.Email.ToLower(),
                    PasswordHash = Hashing.GetPasswordHash(dto.Password)
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }
            public void Update(UserUpdateDto dto)
            {
                User user = _context.Users.Single(u => u.Email == dto.Email.ToLower());
                user.Username = dto.Username;
                user.Email = dto.Email;
                user.PasswordHash = Hashing.GetPasswordHash(dto.Password);
                _context.SaveChanges();
            }
            public void Delete(UserDeletionDto dto)
            {
                User userToDelete = _context.Users.Single(u => u.Email == dto.Email);
                _urlService.DeleteByUser(userToDelete.Id);
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
