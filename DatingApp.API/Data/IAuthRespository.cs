using System.Threading.Tasks;
using DatingApp.API.Models;
namespace DatingApp.API.Data
{
    public interface IAuthRespository
    {
         Task<UserDatingApp> Register(UserDatingApp user, string password);

         Task<UserDatingApp> Login(string userName,string password);

         Task<bool> UserExists(string userName);
    }
}