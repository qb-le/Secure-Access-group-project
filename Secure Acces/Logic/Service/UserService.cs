using Logic.Dto;
using Logic.Interface;

namespace Logic.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<DtoUser> GetAllUsers() => _userRepository.GetAllUsers();
    }
}

