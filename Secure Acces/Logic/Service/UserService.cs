using Logic.Dto;
using DAL;

namespace Logic.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(string connectionString)
        {
            _userRepository = new UserRepository(connectionString);
        }

        public DtoUser GetAllUsers(int employeeId)
        {
            string name = _userRepository.GetUserNameById(employeeId);
            return new DtoUser
            {
                Id = employeeId,
                Name = name
            };
        }
    }
}

