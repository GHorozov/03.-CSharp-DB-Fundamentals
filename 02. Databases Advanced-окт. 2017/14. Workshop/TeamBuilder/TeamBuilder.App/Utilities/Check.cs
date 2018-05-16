namespace TeamBuilder.App.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Check
    {
        public static void CheckLenght(int expectedLenght, string[] array)
        {
            if (expectedLenght != array.Length)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
        }

        public static bool isPasswordValid(string password)
        {
            if (password.Length < 6 || password.Length > 30)
            {
                return false;
            }

            var isContainDigit = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsDigit(password[i]))
                {
                    isContainDigit = true; break;
                }
            }

            var isContainUppercaseLetter = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (Char.IsUpper(password[i]))
                {
                    isContainUppercaseLetter = true; break;
                }
            }

            if (!isContainDigit || !isContainUppercaseLetter)
            {
                return false;
            }

            return true;
        }
    }
}