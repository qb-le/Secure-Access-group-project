using System;
using System.Collections.Generic;
using Logic.Dto;


namespace Logic.Interface
{
    public interface IUserRepository
    {
        List<DtoUser> GetAllUsers();
        string GetUserById(int userId);
    }
}
