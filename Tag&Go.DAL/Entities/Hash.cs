
namespace Tag_Go.DAL.Entities
{
    public static class Hash
    {
        public static string HashPassword(string pwd)
        {
            return BCrypt.Net.BCrypt.HashPassword(pwd);
        }
        public static bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
}
