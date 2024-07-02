namespace scr
{
    public class TypesRequests
    {
        
        public class Login
        {
            // public string login;
            // public string password;
        }
    
        public enum AuthOfRequests
        {
            None,
            Login,
        }

        public class LoginAnswer
        {
            public string token;
            public string refreshToken;
            public bool twoFactorEnabled;
        }
    }
}