namespace eBookStore.Service
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }
        string UserEmail { get; }
        bool IsAdmin { get; }
        void SetAuthenticationStatus(bool isAuthenticated, string userEmail, bool isAdmin);
        void ClearAuthenticationStatus();
    }

    public class AuthService : IAuthService
    {
        public bool IsAuthenticated { get; private set; }
        public string UserEmail { get; private set; }
        public bool IsAdmin { get; private set; }

        public void SetAuthenticationStatus(bool isAuthenticated, string userEmail, bool isAdmin)
        {
            IsAuthenticated = isAuthenticated;
            UserEmail = userEmail;
            isAdmin = isAdmin;
        }

        public void ClearAuthenticationStatus()
        {
            IsAuthenticated = false;
            UserEmail = null;
        }
    }
}
