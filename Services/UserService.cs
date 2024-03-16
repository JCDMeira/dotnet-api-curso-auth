using API.Models;

namespace API.Services
{
    public class UserService
    {
        public bool ValidateUser(UserViewModel userViewModel)
        {
            return userViewModel.UserName == "admin" && userViewModel.Password == "1010";
        }
    }
}
