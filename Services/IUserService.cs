using csharp_demo_backend.Models;

namespace csharp_demo_backend.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        void CreateUser(UserRegistrationDto registrationDto);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public void CreateUser(UserRegistrationDto registrationDto)
        {
            var user = new User
            {
                Username = registrationDto.Username,
                Email = registrationDto.Email,
                Password = registrationDto.Password
                // Set other user properties
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
