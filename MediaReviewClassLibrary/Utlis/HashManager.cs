﻿namespace MediaReviewClassLibrary.Utlis
{
    public static class HashManager
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
