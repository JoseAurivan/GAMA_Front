namespace FrontEnd_GAMA.ViewModels
{
    public class AdminViewModel
    {
        private readonly string Login = "admin";
        private readonly string Password = "LGHZ123**@ABC";

        public string login { get; set; }
        public string password { get; set; }

        public bool ComparePasswordAndLogin()
        {
            if(Login.Equals(login) && Password.Equals(password)) 
            {
                return true;
            }
            return false;
        }
    }
}
