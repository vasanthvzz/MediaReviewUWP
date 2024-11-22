namespace MediaReviewClassLibrary.Utlis
{
    public static class AuthManager
    {
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
