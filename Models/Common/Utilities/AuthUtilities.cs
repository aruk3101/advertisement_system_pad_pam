using Projekt.Models.Common.Enumerated;
using Projekt.Models.Entities;
using Projekt.Models.Repositories;
using Projekt.ViewModels;
using System.Runtime.CompilerServices;

namespace Projekt.Models.Common.Utilities
{
    public class AuthUtilities
    {
        public static int LoggedInUserId { get; private set; }
        public static Role LoggedInUserRole { get; private set; }

        private UserRepository userRepository;
        private static AppShellViewModel _appShellViewModel;

        public AuthUtilities(UserRepository userRepository, AppShellViewModel appShellViewModel)
        {
            this.userRepository = userRepository;
            _appShellViewModel = appShellViewModel;
        }

        public async Task<bool> LogIn(string email, string password)
        {
            User user = await userRepository.FindByEmailAndPassword(email, password);
            if (user != null)
            {
                LoggedInUserId = user.Id;
                LoggedInUserRole = user.Role;
                _appShellViewModel.User = user;
                if (user.Role == Role.ADMIN) _appShellViewModel.IsAdmin = true;
                return true;
            }
            else
            {
                LogOut();
                return false;
            }
        }
        public static void LogOut()
        {
            LoggedInUserId = 0;
            LoggedInUserRole = Role.UNDEFINED;
            _appShellViewModel.IsAdmin = false;
        }

        public async Task<User> GetLoggedInUser()
        {
            if (LoggedInUserId == 0) return null;

            return await userRepository.FindById(LoggedInUserId);
        }

        public async Task SetupAdminAccount()
        {
            if (await userRepository.FindAdminUser() == null)
            {
                User user = new User()
                {
                    Email = "admin@admin.com",
                    PasswordHash = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Role = Role.ADMIN
                };
                await userRepository.AddUserAsync(user);
            }
        }
    }
}
