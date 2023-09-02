namespace csharp_demo_backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // Other user properties

        // Navigation properties for relationships with other entities
        // ...
    }

    public class UserRegistrationDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        // Other registration properties
    }

    public class UserRepository
    {
        private List<User> users;

        public UserRepository()
        {
            users = new List<User>();
        }

        public void AddUser(UserRegistrationDto userDto)
        {
            User user = new User
            {
                Id = GenerateUserId(),
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password
            };

            users.Add(user);
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        private int GenerateUserId()
        {
            // Logic to generate a unique user id
            // ...
        }
    }
}