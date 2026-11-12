namespace SomwhangShop.Services
{
    public class AdminStateService
    {
        // ค่าเริ่มต้นเป็น false เสมอ
        public bool IsLoggedIn { get; private set; } = false;

        public void Login()
        {
            IsLoggedIn = true;
        }

        public void Logout()
        {
            IsLoggedIn = false;
        }
    }
}