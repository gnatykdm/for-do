using System;
using Model;
using System.Collections;

namespace Models.Service
{
    public interface IUserService
    {
        void SaveUser(UserEntity user);
        void UpdateUser(UserEntity user);
        UserEntity? GetUserById(int id);
        List<UserEntity>? GetAllUsers();

        bool findUserByEmail(string email);
        bool findUserByUserName(string userName);
    }
}