using CommunityToolkit.Maui.Converters;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models.Common.Utilities
{
    public class AuthUtilities
    {
        public static int LoggedInUserId { get; private set; }

        private UserRepository userRepository;
        public AuthUtilities(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> LogIn(string email, string password)
        {
            User user = await userRepository.FindByEmailAndPassword(email, password);
            if (user != null)
            {
                LoggedInUserId = user.Id;
                return true;
            }
            else
            {
                LoggedInUserId = 0;
                return false;
            }
        }
        public static void LogOut()
        {
            LoggedInUserId = 0;
        }
    }
}
