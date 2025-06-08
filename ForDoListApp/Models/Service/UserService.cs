using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using ForDoListApp.Data;
using Model;

namespace Models.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDbContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void SaveUser(UserEntity user)
        {
            if (user == null)
            {
                _logger.LogError("User[Save] - user is null.");
                return;
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            _logger.LogInformation("User[Save] - {Username} was saved.", user.Username);
        }

        public void UpdateUser(UserEntity user)
        {
            if (user == null)
            {
                _logger.LogError("User[Update] - user is null.");
                return;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
            _logger.LogInformation("User[Update] - {Username} was updated.", user.Username);
        }

        public UserEntity? GetUserById(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("User[GetUserById] - invalid id.");
                return null;
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                _logger.LogWarning("User[GetUserById] - user with id {UserId} not found.", id);
            }

            return user;
        }

        public List<UserEntity>? GetAllUsers()
        {
            var users = _context.Users.ToList();

            if (users == null || users.Count == 0)
            {
                _logger.LogWarning("User[GetAllUsers] - user list is empty.");
                return null;
            }

            return users ?? new List<UserEntity>();
        }

        public bool findUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogError("User[findUserByEmail] - username is null or empty.");
                return false;
            }

            return _context.Users.Any(u => u.Username == userName);
        }

        public bool findUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogError("User[findUserByEmail] - email is null or empty.");
                return false;
            }

            return _context.Users.Any(u => u.Email == email);
        }
    }
}
